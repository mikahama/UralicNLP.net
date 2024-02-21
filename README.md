# UralicNLP.net

## Download models

Before using the library, you will need to download the models for each language.

    var nlp = new UralicApi();
    nlp.Download("fin");

Just pass the 3-letter ISO code to the *Download* function. You can find [a list of supported languages in the model catalog](http://models.uralicnlp.com/nightly/index.html)

## Morphological analysis

You can analyze words' morphology like this

    var nlp = new UralicApi();
    var analyses = nlp.Analyze("voita", "fin");
    foreach (var analysis in analyses){
        Console.WriteLine(analysis.ToString());
    }

*Analyze* takes in a word form and a language code. It returns a list of *Result* objects. The output of the previous code is:

    vuo+N+Pl+Par
    voi+N+Sg+Par
    voi+N+Pl+Par
    voittaa+V+Act+Ind+Prs+ConNeg
    voittaa+V+Act+Imprt+Prs+ConNeg+Sg2
    voittaa+V+Act+Imprt+Sg2
    voitaa+V+Act+Ind+Prs+ConNeg
    voitaa+V+Act+Imprt+Prs+ConNeg+Sg2
    voitaa+V+Act+Imprt+Sg2

## Lemmatization

Sometimes you only want to lemmatize words.

    var nlp = new UralicApi();
    var analyses = nlp.Lemmatize("voita", "fin");
    foreach (var analysis in analyses){
        Console.WriteLine(analysis);
    }

This outputs:

    vuo
    voi
    voittaa
    voitaa

For compounds, it is possible to mark the word boundaries:

    var nlp = new UralicApi();
    var analyses = nlp.Lemmatize("luutapiiri", "fin", true);
    foreach (var analysis in analyses){
        Console.WriteLine(analysis);
    }

Output:

    luu|tapiiri
    luuta|piiri

## Generation

You can also inflect words given their lemma and morphology

    var nlp = new UralicApi();
    var analyses = nlp.Generate("koira+N+Sg+Par", "fin");
    foreach (var analysis in analyses){
        Console.WriteLine(analysis.ToString());
    }

Output:

    koiraa

## Tokenization

You can tokenize text like this:

    var tokenizer = new Tokenizer();
    var text = "An example sentence. Another, super cool sentence!";
    var tokens = tokenizer.Tokenize(text);
    foreach (var sentence in tokens)
    {
        foreach (var word in sentence)
        {
            Console.Write(word + " ");
        }
        Console.WriteLine();
    }

Output:

    An example sentence . 
    Another , super cool sentence ! 

If you want to only do sentence or word tokenization, you can use Tokenizer.Sentences("text") and Tokenizer.Words("text") respectively
