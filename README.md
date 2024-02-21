# UralicNLP.net

UralicNLP is an NLP (Natural Language Processing) library for several endangered Uralic languages. In addition, the library supports larger languages like Finnish, English, Swedish, Spanish, German, Italian, French, Russian and so on. The library is developed by [Mika Hämäläinen](https://mikakalevi.com)

You might also be interested in [the Python version of UralicNLP](https://github.com/mikahama/UralicNLP).

## Installation

You can install UralicNLP from [NuGet](https://www.nuget.org/packages/UralicNLP/) like this:

    dotnet add package UralicNLP

After installing UralicNLP, you can import it like this:

    using UralicNLP;

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

If you want to only do sentence or word tokenization, you can use Tokenizer.Sentences("text") and Tokenizer.Words("text") respectively.

## Cite

If you use UralicNLP in an academic publication, please cite it as follows:

Hämäläinen, Mika. (2019). UralicNLP: An NLP Library for Uralic Languages. Journal of open source software, 4(37), [1345]. https://doi.org/10.21105/joss.01345

    @article{uralicnlp_2019, 
        title={{UralicNLP}: An {NLP} Library for {U}ralic Languages},
        DOI={10.21105/joss.01345}, 
        journal={Journal of Open Source Software}, 
        author={Mika Hämäläinen}, 
        year={2019}, 
        volume={4},
        number={37},
        pages={1345}
    }

For citing the FSTs and CGs, see *UralicApi.ModelInfo(language)*.

The FST and CG tools and dictionaries come mostly from the [GiellaLT repositories](https://github.com/giellalt) and [Apertium](https://github.com/apertium).
