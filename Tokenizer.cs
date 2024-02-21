
using System.Text.RegularExpressions;

namespace UralicNLP
{
    public class Tokenizer
    {
        private String SentenceEnd = "!?。……‥！？。⋯…؟჻!…";
        private String WordEndPuct = ",;:”’'\"»」)]}،؛》』〕｠〉》】〗〙〛–—";
        private String WordStartPunct = "'\"¡¿「«“”‘({[《『〔｟〈《【〖〘〚–—”";
        private String Numbers = "0123456789١٢٣٤٥٦٧٨٩٠";
        private String CustomPunctuation = "!\"#$%&'()*+,-.:;<=>?@[]^_`{|}~";
        private Regex abrvRegex;

        public Tokenizer()
        {
            List<string> abb = new List<string> { "rec", "т.г", "t.a.m", "m.i", "h.r", "b.b", "utt", "griech", "kl", "poc", "cm", "jan", "l.a", "eritt", "kft", "eccl", "nov", "þ", "pe", "a.u.m", "a.g", "jer", "s.a", "c..", "maks", "дун", "h.u.c", "u.s.w", "ft", "soddisfatt", "ca", "dipl", "ժթ", "responsabilit", "ri", "su", "vol", "bennett#harj", "प्रो", "ընդհ", "b.v", "e.v", "आर", "eteisper", "i.v", "xlvii", "sens", "զ", "tml", "a.ş", "u.c", "u.tml", "tr", "yapıyorlar", "governa", "brah", "pkl", "m", "lv", "vs", "f.a", "प्रा", "n.l", "bush'tur", "g.o.f", "c.m", "tzv", "b.i.p", "xiv", "az", "i.d.s", "ș.a", "я", "söyledi", "huom", "taglia", "ekr", "int", "kg", "ը", "var", "ин", "mm", "con", "ģ", "red.anm", "m.fl", "exo", "nyk", "kcl#inf", "zzgl", "c.a.r", "geliyor", "ot.prp", "sottosegreta", "sce", "phys", "пахонино", "டி.எஸ்", "r.r", "ш", "evtl", "qual", "பா.ம.க", "i.w", "non", "cav", "j.-y", "ס", "róm", "ул", "c.i.a", "սրկ", "h.s", "g", "ет", "georg", "амер", "фр", "amministra", "जी", "էֆ", "madam", "red", "p.a", "att", "psyykk", "hab", "s.e", ".__", "t.a", "steht", "o.v", "wollen", "қ", "f.u.n", "o.g.m", "t.i.a", "katılmadı", "d.c", "j.g", "interkom", "sog", "c.h", "h.a.l", "l.w.s.a.l.b", "croll", "ogg", "о", "şekilde", "iacob", "k.g", "βλ", "r.a", "ப", "toist", "ե", "sis", "ny", "d.h", "ltd", "historias", "ஜி.எல்", "semif", "ediyordu", "ed", "δισ", "v.v.d", "navigan", "ž", "b.a", "mrs", "ii", "ⲉ..ⲙ.", "dr", "мін", "m.a.w", "k", "w", "#istituo", "e.t", "рр", "plm", "be", "դպ", "doc", "o.d.s", "ok", "fax", "e.p", "švč", "saks", "எம்.எஸ்ஸி", "g.m", "a.f", "j.w", "c-à-d", "cit", "s.b", "alt", "abt", "z", "t.u", "пр", "rev", "h.g", "krs", "kon", "κκε", "us-nr", "x", "bzw", "p", "dc", "sras", "ezekh", "esim", "n.w", "і", "pom", "açmadım", "qd", "g.i", "fa", "matt", "değişmedi", "rchb.f", "т.н", "г", "info", "β", "γ", "гр", "dr.med", "pte", "pont", "n.n", "kgl", "ए", "r.a.v.e", "syr", "sever", "бел", "heb", "dec", "बी", "डब्लू", "tasarlanmıştır", "treten", "hós", "j.d.p", "ε.ε", "akt", "l.p", "c.j", "tél", "kal", "nr", "вип", "j.c", "pan", "messrs", "y.w", "գ", "fung", "f.eks", "रेत", "a.p.s", "yok", "burg", "ν", "d.o.m", "i.c", "tedes", "at&t", "plc", "ug", "n.b.a", "interrog", "kap", "ry", "g.u", "xxiv", "mhgr", "pét", "сер", "attı", "ff", "plkst", "þess", "o.a", "resp", "एम", "ā", "ml", "стр", "a.iii", "j.j", "эльбинг", "xxvi", "nn", "pdes", "e.d", "páx", "para", "greenwich/conn", "m.cr", "que", "g.p", "h.r.f", "маль", "pc", "et", "p.j.m", "._", "פ", "juris", "s.k.a.t.e", "kungl", "ricove", "g.d.p", "pf./min", "g.o", "iyi", "lda", "տոքթ", "s.o.s", "ազգ", "r.b.t", "xi", "mwst", "r.m", "www", "g.e", "kr", ":/", "vii", "b.m", "itd", "pot", "p.d.c", "е", "xlv", "vind", "k.k", "proc", "շ", "h.-g", "decembr", "c.s", "c.c", "i.m", "т.п", "s.p", "okt", "դ", "esp", "satur", "yay", "s.c", "ift.tt", "sec", "exod", "o/m", "μ.μ", "npr", "mov", "сац", "es", "a.s.o", "m.sh", "t.s", "e.a.t", "ы", "joh", "bros", "h.l", "f", "s.f", "ley", "r.y", "jne", "f.v", "st", "'", "flg", "պ", "d.a.p", "h.e.r", "vv", "трлн", "mo", "एल", "пор", "pvt", "म.प्र", "a.m.k", "ș", "подп", "jap", "journ", "u", "janv", "hita", "krf", "о.з", "v.j", "угорськ", "mån", "ծն", "op", "so", "m.h", "xlvi", "lúk", "द", "mi", "a.b.p", "c.iii", "c.a", "a.b.c", "él", "u.s.o", "ds", "l.h", "mln", "mr", "r", "h.f", "liv", "detroit-st", "напр", "o.s.v", "ifj", "r.i.d", "в", "al", "w.e", "corp", "s.u.s.e", "i.n", "frk", "t.j", "s.g", "mevr", "bl.a", "ass't", "l.o", "kapituli", "tn", "lb", "जी.टी", "manifesta", "p.g", "totalmente", "pnt", "грец", "r.c", "bay", "etj", "է", "i.d", "ø", "feat", "зуб", "guid", "ім", "доц", "res", "r.k", "sts", "соп", "approx", "c.g", "pst", "uimh", "koyar", "lt", "jóh", "jak", "b.iii", "sundu", "mc", "heng", "б.з.б", "stationsstraat-ged", "consigl", "a.b.d", "viv", "kapanık", "ст.н.с", "ver", "nl", "johs", "ל", "ou", "t.h", "տիկ", "carga", "sare", "n.s", ".i", "գերշ", "bzgl", "http://www.quitandwin.org)", "com", "cannons", "κκ", "δρ", "քհնյ", "e.u.a", "g.g", "k-x", "e.r", "μ", "per", "फिल", "w.i", "kt", "yüksek", "apr", "ehem", "sv", "xliv", "sc", "diffi", "co", "deut", "благов", "a.u.b", "deutr", "ն", "aug", "j.-p", "ac", "p.s", "c.l.r", "etti", "buon", "tu", "l.e.d", "н", "टी", "will", "gld", "т.о.к", "հ", "giorn", "m.c", "xvii", "ggf", "peri", "bv", "e.c", "j.b", "di", "e.g.b", "s.w.i.f.t", "lii", "ін", "a.v", "attn", "i.g", "зв", "cie", "zof", "v.c", "и.о", "i.k.u", "год", "u.d", "a.d", "डी", "i.r", "मि", "խ", "t.n.v", "எஸ்.எம்", "çıktı", "ariz", "eg", "h.p", "s.w.a.t", "дж", "ש", "soc", "pehmeä#", "kokonaan", "pl", "s.k", "उ.प्र", "आई", "edell", "mv", "dic", "εκ", "b.o.b", "osb", "alk", "e.m", "λ.χ", "j.j.a", "konl", "matth", "юґосл", "meld", "frp", "губ", "tít", "जे", "a.m", "cep", "cordarone#inf", "бл", "है", "oll", "gr", "부르+었+다", "c.p", "π.μ", "семигород", "parlamentul_european", "h.m.s", "mgr", "ne", "etc", "c.p.c", "p.u.f", "o.s.frv", "bh", "lic", "anm", "wm", "palazz", "լ", "என்", "α.ε", "ruots", "n.o", "pdas", "o.m", "dvs", "umr", "vgs", "s.a.r.a.c", "el", "c.v", "ınc", "j.w.m", "ph.d", "št", "uğramıştım", "սեպտ", "d.o.c", "subsp", "mrd", "f.e", "л", "abk", "j.p", "filipp", "d.t", "u.þ.b", "b.a.t", "t.o.v", "arrv", "мр", "šv", "கே.பி.பி", "r.h", "circ", "f.kr", "e.h", "ww", "ғ", "ang", "j.m", "val", "nei", "mag", "j.a", "s.p.a", "e.n.i", "xxxiii", "կրթ", "sext", "m.s.n.m", "драм", "சி", "b.c.e", "app", "heinrich-hertz-str", "zor", "rp", "ст", "toiv", "գերպ", "jf", "ej", "т.м", "j.r.r", "o.j", "zn", "лв", "xii", "llc", "долл", "eks", "j.d", "istemezsin", "tel", "s.r", "u.t.t", "p-r", "jesaj", "c.b.é", "e.t.a", "մոնս", "a.v.p", "mlle", "ք", "g.b", "mobilis", "ns", "i.b", "l.f", "סט", "ex", "ymsgr:sendim?mayursha&__hi+mayur", "s.h", "e.n.o.t", "bilmiyorlar", "தா", "oz", "čl", "մագ", "u.a", "д", "prof", "т.б", "biv", "a.k.a", "kongl", "f.h", "bo", "vyr", "а.к.а", "ned", "depto", "th", "u.n", "c", "igualar", "on", "p.m", "t.g.v", "asoc", "antimaf", "siliconvalley.com", "mht", "ing", "f.w", "me", "c.i", "z.m", "jr", "к", "ave", "a", "tas", "տ", "pf/min", "non.iun", "xvi", "a.v.g", "naz", "verensok", "p.o.w", "belirsizdir", "ques", "t.g.f", "hengitys#fr", "n.-br", "h.t", "o.t.o", "xlix", "j", "eaa", "trave", "verdi", "l.s", "արժ", "d.j", "a.r", "m.g", "sid", "spp", "κα", "ஜி", "vi", "č", "fla", "յունվ", "tímóth", "i.e", "başaramadı", "ه.ق", "εε", "сл", "ts", "mak", "կ", "k.-h", "w.h.s", "alabilirsiniz", "yms", "ym", "aplo", "adj", "piem", "m.i.t", "κ", "eco", "k.p", "எஸ்.எஸ்", "prp", "ev", "job", "vgl", "արք", "o.r", "inkl", "j.l", "yaşamazdım", ".", "երեսփ", "полк", "h.m", "k.o", "ս", "h.h", "eur", "grl", "@.", "m.w", "கி", "கே.எஸ்", "c.r", "one", "casin", "reg", "pp", "பழ", "tra", "gara", "mark", "junc", "π.χ", "k.c", "öğreneceğim", "olabilir", "s.u.a.p.s", "art", "olacak", "osv", "dwz", "інш", "deutro", "n.v", "god", "änkytt", "articol", "novembr", "m.j.e.m", "и", "r.u", "u.s", "xliii", "pr", "innst", "акад", "константинопольский", "hndr", "niv", "ch", "мик", "ukr", "ժէ", "tūkst", "þ.b", "www.uno-e", "evt", "c.p.p.p", "angl", "em", "conf", "οηε", "є", "s.a.t", "pt", "gibiydi", "e.t.o", "d.o.o", "entsp", "convar", "mfl", "a.l.f.a", "vas", "բ", "@", "incl", "у", "t.a.v", "max", "n.chr", "o.j.a.m", "s.a.d", "किमी", "b.c", "cum", "conn", "altr", "xx", "b.f", "tms", "handr", "ஐ.பி.எல்", "e.w", "gepr", "с.в", "l", "х", "р", "ha", "ont", "p.j", "մ", "a.s", "br", "febr", "cos", "prop", "ef", "prior", "c.ii", "çıkardık", "past", "c.t", "quint", "aa", "col", "arh", "orami", "م", "передача", "mahd", "s.s", "o.n.u", "frz", "söylüyor", "f.n", "என்.எல்.சி", "xviii", "ओ", "t.ex", "агентура", "va", "in.", "port", "தி.மு.க", "зах", "v.b", "s", "mek", "bl", "e", "insbes", "s.d", "jos", "blvd", "b.o", "p.p.s", "ga", "fazla", "क्यू", "др", "prof-dr", "rdr", "km/t", "р.х", "capt", "վրդ", "k.n.s.m", "टेक", "lib", "d.w", "нім", "personal", "iv", "p.c", "s.a.m", "օ", "añs", "т", "e.n.m", ".-", "փրոֆ", "dhr", "ar.co", "кол", "ms", "v . h ", "dr.-ing", "dat", "εκατ", "rivo", "notables", "англ", "sto", "भी", "i", "b.t", "lavor", "hemo#dyn", "maias", "μ.χ", "l.l", "b.i", "corr", "и.д", "o.l", "тер", "mil", "bouw-c.a.o", "j.b.o", "#protezion", "tsankov", "eu", "aloit", "q.e.p.d", "termin", "dr.psychol", "pvz", "ஐ.நா", "дол", "t.i", "viii", "ptas", ",", "l.j", "inc", "e.kr", "o", "vrk", "ricevu", "ph", "no", "ks", "srl", "convint", "http://t", "sen", "r.d", "arts", "kone#fr", "n.e.br", "सी", "pre", "m.v", "वाई", "nom", "m.l", "лат", "e.a.r", "oik", "ծ", "m.c.c", "maxiemenda", "kht", "o.u.a", "ա", "d.v.s", "t.t", "vorre", "ext", "y", "s.l", "a.m.l", "млрд", "u.t", "abs", "g.k", "ky", "бул", "n.j", "t.s.a", "ժ", "distr", "d.i", "ф", "кв", "j.-c", "f.j.g", "cf", "kór", "j.v", "devt", "tbk", "քհյն", "f.r.s", "к.с", "m.u.s.e.u.m", "t.n.t", "h.q", "եպիսկ", "t.i.m", "lat", "б.в", "v.c.i", "e.g", "gen", "ред", "w.h", "h", "ud", "кер", "lp", "டி", "r.m.r", "jkr", "mill", "v", "s.a.r.l", "mos", "օր", "st.meld", "reddeder", "sr", "politi", "g.j", "c.l", "літ", "s.n", "m.a", "žin", "p.e.v", "xv", "bp", "m.ag", "can", "a.l.f", "ஐ.ஏ.எஸ்", "v.a", "वी", ".ⲟ", "проф", "commons", "hemo#dynam", "por.l", "с", "bulmalı", "erit", "a.p", "sl", "xiii", "spec", "relig", "एच", "jesai", "n", "թաղ", "sit.", "mass", "fund", "суч", "f.b.i", "c.d", "u.e.r", "o.n.c.e", "італ", "dell", "dem", "sk", "ftpx", "dz", "kol", "mlrd", "т.нар", "fig", "fil.mag", "l.i", "organismus", "צ", "բրկ", "з", "xix", "po", "dan", "мал", "g.s", "jj.oo", "a.r.e", "oa", "u.ä", "hr", "q", "αρ", "ап", "cc", "😂", "h.e", "թ", "milj", "fas", "d.l", "jūn", "f.a.z", "e.k", "o.p", "p.i.b", "ஏ.எம்", "ամեն", "т.е", "ir", "reigate", "б", "pweination", "c.f", "sept", "qu", "pga", "mio", "vid", "liii", "p.h", "mej", "sextil", "kor", "ass", "f.l", "rt", "n.y", "іст", "hl", "stolt", "t.z", "san", "e.r.c.m", "млн", "реж", "डब्ल्यू", "के", "min", "kath", "zurück", "lindl", "t", "r.i.p", "chr", "s.r.i.a", "gov", "डॉ", "л.c", "cal", "a.o", "n.c", "ņ", "pass", "þ.e", "marquin", "şey", "zypper", "एस", "doce", "m.sc", "டி.எம்", "м", "а", "oluşturdu", "r.c.d", "б.т.с", "a.c", "f.o.b", "j.s.r", "minis", "relativ", "гос", "adm.dir", "вул", "velenoso", "ई", "u.k", "dra", "nk", "best.nr", "d", "sydänkir", "iyidir", "syst", "ε.α.μ", "remaster", "изд", "z.b", "n.c.r", "к.п.н", "suom", "j.f.k", "диф", "penn", "hos", "diyoruz", "k.u.k", "реєстр", "тис", "rilas", "tj", "sal", "engl", "vb", "v.d", "oops", "o.fl", "levit", "a.r.m.o.r", "r.-g", "ж", "пл", "серж", "subit", "jkv", "hebr", "jour", "ap", "ghz", "дз", "எம்.பி", "n.b", "aprox", "str", "एन", "b.r", "t.d", "isl", "psalm", "inca", "cc.oo", "u.s.a", "կրօն", "ec", "gmd/st", "ahd", "o.k", "mij", "fr", "m.l.r", "m.b", "š.g", "dr.philos", "c.b", "etab", "c.i.d", "कि.मी", "d.d", "mob", "чл", "мыс", "jūl", "entspr", "р.ф", "c.s.d", "यू", "sp", "எல்.என்", "mikh", "m.m", "ю", "jon", "j.s.g", "comme", "b.d", "a.a", "o.s", "li", "h.c", "w.s", "cap", "š", "f.r", "cand.polit", "säv", "tālr", "ecc", "quiri", "a.u", "स्व", "est", "gal", "mind", "ஆர்", "וו", "usw", "alþ", "segreta", "uitgeversmij", "prep", "f.c", "ala", "ал", "üstünde", "lgh", "dž", "a.h", "pop", "inf", "t.o.m", "prov", "டி.வி", "vo", "b.ii", "gs", "ko", "mar", "c.c.c", "mėn", "eds", "istedim", "vlč", "d.e", "bayan", "m.c.b", "c.q", "f.m", "ot", "sat", "sud.", "olie", "gbit/sq.in", "eve", "י", "jesa", "harj", "б.а", "p.r", "спэц", "b", "tp", "c.m.l.g", "κλπ", "o.g", "t.a.c", "hermann-j", "e.c.u", "mg", "коп", "gördü", "m.a.l.i.c.i.a", "lpp", "rep", "αριθ", "m.s", "ven", "a.t", "d.c.a", "edizio", "ընկ", "e.e", "ісп", "urr", "v.chr", "мин", "डा", "hnd", "drs", "joo#", "தொ.மு.ச", "ई.पू", "св", "м.г", "apok", "sett", "fr.o.m", "tele", "lo", "z.i", "pas", "b.o.n.d", "tím", "պր", "gebr", "görünüyor", "n.r.m", "wa", "dom", "idv", "med", "görüyor", "դոկտ", "ad", "hospitalet-u", "mümkün", "ч", "ek", "pa", "la", "r.e.m", "э", "бв", "mt", "див", "noradr", "urb", "m.d", "anton", "б.р", "çoğalıyor", "χλμ", "colo", "l.sin", "պրն", "xlviii", "ps", "ee.uu", "पी", "jhr", "''dır", "i.s.c", "கே", "üstleniyordu", "p.o", "xxi", "t.v", ".j", "diyor", "männer", "ix", "par", "п", "iii", "nj", "soul", "m.ö", "κ.κ.ε", "c.k", "simul", "pb", "c.u.r" };
            List<string> abbreviations = new List<string>();
            foreach (string ab in abb)
            {
                abbreviations.Add(Regex.Escape(ab));
            }
            string regex = "(^|\\s)(" + string.Join("|", abbreviations) + ")$";
            abrvRegex = new Regex(regex);
        }

        public List<List<string>> Tokenize(string text)
        {
            List<List<string>> result = new List<List<string>>();
            List<string> sents = Sentences(text); // Assuming Sentences is a method returning List<string>
            foreach (string sent in sents)
            {
                result.Add(Words(sent)); // Assuming Words is a method returning List<string>
            }
            return result;
        }

        private bool EndsInAbrv(string text)
        {
            Match match = abrvRegex.Match(text.ToLower());
            return match.Success;
        }

        public List<string> Words(string text)
        {
            Regex multidot = new Regex("(\\.{2,})$");
            foreach (char sentenceEndChar in SentenceEnd)
            {
                string sentenceEndP = Regex.Escape(sentenceEndChar.ToString());
                text = Regex.Replace(text, sentenceEndP, " " + sentenceEndP);
            }
            text = text.Trim().Replace("\\s+", " ");
            string[] whitespaceTokens = text.Split(' ');
            List<string> tokens = new List<string>();
            foreach (string whitespaceToken in whitespaceTokens)
            {
                string t = whitespaceToken;
                List<string> firstTok = new List<string>();
                List<string> lastTok = new List<string>();
                bool contFirst = true;
                while (contFirst)
                {
                    contFirst = false;
                    if (t.Length > 0 && WordStartPunct.Contains(t[0].ToString()))
                    {
                        contFirst = true;
                        firstTok.Add(t[0].ToString());
                        t = t.Substring(1);
                    }
                }

                bool contLast = true;
                while (contLast)
                {
                    contLast = false;
                    if (t.Length > 0 && WordEndPuct.Contains(t[t.Length - 1].ToString()))
                    {
                        contLast = true;
                        lastTok.Insert(0, t[t.Length - 1].ToString());
                        t = t.Substring(0, t.Length - 1);
                    }
                    else if (t.Length > 1 && t[t.Length - 1] == '.' && WordEndPuct.Contains(t[t.Length - 2].ToString()))
                    {
                        contLast = true;
                        lastTok.Insert(0, t[t.Length - 1].ToString());
                        lastTok.Insert(0, t[t.Length - 2].ToString());
                        t = t.Substring(0, t.Length - 2);
                    }
                }

                Match m = multidot.Match(t);
                if (m.Success)
                {
                    string dots = m.Value;
                    lastTok.Insert(0, dots);
                    t = t.Substring(0, t.Length - dots.Length);
                }
                else if (t.Length > 0 && t[t.Length - 1] == '.')
                {
                    // Assuming endsInAbrv is a method you have for checking abbreviations
                    if (!EndsInAbrv(t.Substring(0, t.Length - 1)))
                    {
                        t = t.Substring(0, t.Length - 1);
                        lastTok.Insert(0, ".");
                    }
                }

                bool hasCustomPunct = false;
                foreach (char customPunctChar in CustomPunctuation)
                {
                    if (t.Contains(customPunctChar.ToString()))
                    {
                        hasCustomPunct = true;
                        break;
                    }
                }

                string[] tTok;
                if ((t.Contains("/") || t.Contains("\\")) && !hasCustomPunct)
                {
                    t = Regex.Replace(t, Regex.Escape("/"), " /").Replace(Regex.Escape("\\"), " \\");
                    tTok = t.Split(' ');
                }
                else
                {
                    tTok = new string[] { t };
                }

                List<string> tt = new List<string>();
                foreach (string ts in tTok)
                {
                    if (ts.Length > 0)
                    {
                        tt.Add(ts);
                    }
                }

                firstTok.AddRange(tt);
                firstTok.AddRange(lastTok);
                tokens.AddRange(firstTok);
            }

            List<string> returnTokens = new List<string>();
            foreach (string tok in tokens)
            {
                if (tok.Length > 0)
                {
                    returnTokens.Add(tok);
                }
            }
            return returnTokens;
        }

        public List<string> Sentences(string text)
        {
            List<string> parts = new List<string>();
            string currentS = "";
            bool previousBreak = false;
            for (int i = 0; i < text.Length; i++)
            {
                string c = text[i].ToString();
                if (SentenceEnd.Contains(c))
                {
                    if (currentS.Length > 0)
                    {
                        parts.Add(currentS + c);
                        currentS = "";
                    }
                    else if (parts.Count > 0)
                    {
                        parts[parts.Count - 1] = parts[parts.Count - 1] + c;
                    }
                    else
                    {
                        currentS = c;
                    }
                }
                else if (c == ".")
                {
                    if (currentS.Length == 0)
                    {
                        if (parts.Count > 0)
                        {
                            parts[parts.Count - 1] = parts[parts.Count - 1] + c;
                        }
                        else
                        {
                            currentS = c;
                        }
                    }
                    else if (Numbers.Contains(currentS[currentS.Length - 1].ToString()))
                    {
                        currentS += c;
                    }
                    else if (EndsInAbrv(currentS)) // You need to implement EndsInAbrv method
                    {
                        currentS += c;
                    }
                    else if (text.Length > i + 1 && !string.IsNullOrWhiteSpace(text[i + 1].ToString()))
                    {
                        currentS += c;
                    }
                    else
                    {
                        parts.Add(currentS + c);
                        currentS = "";
                    }
                }
                else if (c == "\n")
                {
                    if (previousBreak && currentS.Length > 0)
                    {
                        parts.Add(currentS);
                        currentS = "";
                    }
                    if (!previousBreak && currentS.Length > 0)
                    {
                        currentS += c;
                    }
                    previousBreak = true;
                    continue;
                }
                else if (c == "\r")
                {
                    continue;
                }
                else
                {
                    currentS += c;
                }
                previousBreak = false;
            }
            if (currentS.Length > 0)
            {
                parts.Add(currentS);
            }

            List<string> returnParts = new List<string>();
            foreach (var part in parts)
            {
                var trimmedPart = Regex.Replace(part.Trim(), "\\s+", " ");
                if (trimmedPart.Length > 0)
                {
                    returnParts.Add(trimmedPart);
                }
            }

            return returnParts;
        }

    }
}