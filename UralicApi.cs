using System.Text.RegularExpressions;
using hfst;

namespace UralicNLP
{
    public class UralicApi
    {
        private String ModelPath;
        private String DownloadServerUrl = "http://models.uralicnlp.com/nightly/";

        private Dictionary<string, hfst.HFST> transducerCache = new Dictionary<string, hfst.HFST>();

        public UralicApi()
        {
            ModelPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".uralicnlp");
        }

        public UralicApi(String modelPath)
        {
            ModelPath = modelPath;
        }

        public bool IsLanguageInstalled(string language)
        {
            var languageFolder = Path.Combine(ModelPath, language);
            return Directory.Exists(languageFolder) || File.Exists(languageFolder);
        }

        private HFST LoadTransducer(string language, string filename)
        {
            string languageFolder = Path.Combine(ModelPath, language, filename);
            if (!transducerCache.ContainsKey(languageFolder))
            {
                var transducer = new HFST(languageFolder);
                transducerCache[languageFolder] = transducer;

            }
            return transducerCache[languageFolder];
        }

        public void ModelInfo(string language)
        {
            var languageFolder = Path.Combine(ModelPath, language, "metadata.json");
            using (FileStream fs = new FileStream(languageFolder, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fs, System.Text.Encoding.UTF8))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    Console.WriteLine(str);
                }
            }
        }

        private string GetModelName(bool analyzer, bool descriptive, bool dictionaryForms)
        {
            if (analyzer)
            {
                if (dictionaryForms)
                {
                    return "analyser-dict";
                }
                else if (descriptive)
                {
                    return "analyser";
                }
                else
                {
                    return "analyser-norm";
                }
            }
            else
            {
                if (!descriptive && dictionaryForms)
                {
                    return "generator";
                }
                else if (descriptive)
                {
                    return "generator-desc";
                }
                else
                {
                    return "generator-norm";
                }
            }
        }
        public List<Result> Analyze(string word, string language, bool descriptive, bool dictionaryForms)
        {
            string modelName = GetModelName(true, descriptive, dictionaryForms);
            HFST t = LoadTransducer(language, modelName);
            var analyses = t.Lookup(word);
            return analyses;
        }

        public List<Result> Analyze(string word, string language)
        {
            return Analyze(word, language, true, false);
        }

        public List<Result> Generate(string word, string language)
        {
            return Generate(word, language, false, false);
        }

        public List<Result> Generate(string word, string language, bool descriptive, bool dictionaryForms)
        {
            string modelName = GetModelName(false, descriptive, dictionaryForms);
            HFST t = LoadTransducer(language, modelName);
            var analyses = t.Lookup(word);
            return analyses;
        }

        public List<string> Lemmatize(string word, string language, bool descriptive, bool dictionaryForms, bool wordBoundaries)
        {
            List<string> results = new List<string>();
            var res = Analyze(word, language, descriptive, dictionaryForms);

            string bound = "";
            if (wordBoundaries)
            {
                bound = "|";
            }

            foreach (var ana in res)
            {
                var an = ana.ToString();
                string lemma;
                if (language.Equals("swe"))
                {
                    lemma = Regex.Replace(an, "[<].*?[>]", bound);
                    while (lemma.Length > 0 && bound.Length > 0 && bound[0] == lemma[lemma.Length - 1])
                    {
                        lemma = lemma.Substring(0, lemma.Length - 1);
                    }
                }
                else if (language.Equals("ara"))
                {
                    lemma = StringProcessing.FilterArabic(an, true, bound); // You'll need to implement this method
                }
                else if (language.Equals("fin_hist"))
                {
                    string rege = "(?<=WORD_ID=)[^\\]]*";
                    List<string> allMatches = new List<string>();
                    MatchCollection matches = Regex.Matches(an, rege);
                    foreach (Match match in matches)
                    {
                        allMatches.Add(match.Value);
                    }
                    lemma = string.Join(bound, allMatches);
                }
                else if (an.Contains("<") && an.Contains(">"))
                {
                    // Apertium
                    string[] parts = an.Split('+');
                    List<string> lemmaParts = new List<string>();
                    foreach (var part in parts)
                    {
                        lemmaParts.Add(part.Split('<')[0]);
                    }
                    lemma = string.Join(bound, lemmaParts);
                }
                else
                {
                    if (an.Contains("#") && !an.Contains("+Cmp#"))
                    {
                        an = an.Replace("#", "+Cmp#");
                    }
                    string[] parts = an.Split(new string[] { "+Cmp#" }, StringSplitOptions.None);
                    List<string> lemmaParts = new List<string>();
                    foreach (var part in parts)
                    {
                        string p = part.Split('+')[0];
                        if (language.Equals("eng"))
                        {
                            p = Regex.Replace(p, "[\\[].*?[\\]]", "");
                        }
                        lemmaParts.Add(p);
                    }
                    lemma = string.Join(bound, lemmaParts);
                }
                results.Add(lemma);
            }

            // Use Distinct to remove duplicates and ToList to convert back to List
            return results.Distinct().ToList();
        }

        public void Uninstall(string language)
        {
            string directoryPath = Path.Combine(ModelPath, language);
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }

        public void SupportedLanguages()
        {
            try
            {
                string s = CommonTools.ReadToString(DownloadServerUrl + "supported_languages.json");
                Console.WriteLine(s);
            }
            catch (System.IO.IOException ex)
            {
                Console.WriteLine("An IO exception occurred: " + ex.Message);
                // Optionally, handle the exception here
            }
        }

        public void Download(string language)
        {
            Download(language, true);
        }

        public List<string> Lemmatize(string word, string language)
        {
            return Lemmatize(word, language, true, false, false);
        }

        public List<string> Lemmatize(string word, string language, bool wordBoundaries)
        {
            return Lemmatize(word, language, true, false, wordBoundaries);
        }


        public void Download(string language, bool showProgress)
        {
            var urlMap = new Dictionary<string, string>
            {
                ["analyser"] = "analyser-gt-desc.hfstol",
                ["analyzer.pt"] = $"../neural/{language}_analyzer_nmt-model_step_100000.pt",
                ["generator.pt"] = $"../neural/{language}_generator_nmt-model_step_100000.pt",
                ["lemmatizer.pt"] = $"../neural/{language}_lemmatizer_nmt-model_step_100000.pt",
                ["analyser-norm"] = "analyser-gt-norm.hfstol",
                ["analyser-dict"] = "analyser-dict-gt-norm.hfstol",
                ["generator-desc"] = "generator-gt-desc.hfstol",
                ["generator-norm"] = "generator-gt-norm.hfstol",
                ["generator"] = "generator-dict-gt-norm.hfstol",
                ["cg"] = "disambiguator.bin",
                ["metadata.json"] = "metadata.json",
                ["dictionary.json"] = "dictionary.json"
            };

            var languageFolder = Path.Combine(ModelPath, language);
            try
            {
                Directory.CreateDirectory(languageFolder);
            }
            catch (IOException ex)
            {
                // Handle exceptions related to directory creation
                Console.WriteLine("Failed to create directory: " + ex.Message);
            }

            foreach (var entry in urlMap)
            {
                string fileName = entry.Key;
                string urlName = $"{language}/{entry.Value}";
                Console.WriteLine($"Downloading model {fileName} for language {language}");
                try
                {
                    CommonTools.DownloadToFile(DownloadServerUrl + urlName, Path.Combine(languageFolder, fileName), showProgress);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Model wasn't downloaded, it may not exist for this language");
                    if ("metadata.json" == fileName)
                    {
                        try
                        {
                            using (var writer = new StreamWriter(File.OpenWrite(Path.Combine(languageFolder, fileName)), System.Text.Encoding.UTF8))
                            {
                                writer.Write("{\"info\":\"no metadata provided\"}");
                            }
                        }
                        catch (IOException ex1)
                        {
                            // Handle exceptions related to file writing
                            Console.WriteLine("Failed to write file: " + ex1.Message);
                        }
                    }
                }
            }
        }
    }
}