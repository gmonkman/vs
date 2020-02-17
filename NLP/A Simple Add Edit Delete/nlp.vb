Public Class nlp

    Private mModelPath As String = "C:\Development\NLP\SharpNLP\Models\"

    Private mSentenceDetector As OpenNLP.Tools.SentenceDetect.MaximumEntropySentenceDetector
    Private mTokenizer As OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer
    Private mPosTagger As OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger
    Private mChunker As OpenNLP.Tools.Chunker.EnglishTreebankChunker
    Private mParser As OpenNLP.Tools.Parser.EnglishTreebankParser
    Private mNameFinder As OpenNLP.Tools.NameFind.EnglishNameFinder

    Public Function SplitSentences(ByVal paragraph$) As String()
        If mSentenceDetector Is Nothing Then
            mSentenceDetector = New OpenNLP.Tools.SentenceDetect.EnglishMaximumEntropySentenceDetector(mModelPath + "EnglishSD.nbin")
        End If
        SplitSentences = mSentenceDetector.SentenceDetect(paragraph)
    End Function

    Public Function TokenizeSentence(ByVal sentence As String) As String()
        If mTokenizer Is Nothing Then
            mTokenizer = New OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer(mModelPath + "EnglishTok.nbin")
        End If
        TokenizeSentence = mTokenizer.Tokenize(sentence)
    End Function

    Public Function PosTagTokens(ByVal tokens As String()) As String()
        If mPosTagger Is Nothing Then
            mPosTagger = New OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger(mModelPath + "EnglishPOS.nbin", mModelPath + "\Parser\tagdict")

        End If
        PosTagTokens = mPosTagger.Tag(tokens)
    End Function

    Public Function ChunkSentence(ByVal tokens As String(), ByVal tags As String()) As String
        If mChunker Is Nothing Then
            mChunker = New OpenNLP.Tools.Chunker.EnglishTreebankChunker(mModelPath + "EnglishChunk.nbin")
        End If
        ChunkSentence = mChunker.GetChunks(tokens, tags)
    End Function

    Public Function ParseSentence(ByVal sentence As String) As OpenNLP.Tools.Parser.Parse
        If mParser Is Nothing Then
            mParser = New OpenNLP.Tools.Parser.EnglishTreebankParser(mModelPath, True, False)
        End If
        ParseSentence = mParser.DoParse(sentence)
    End Function

    Public Function FindNames(ByVal sentence As String) As String
        If mNameFinder Is Nothing Then
            mNameFinder = New OpenNLP.Tools.NameFind.EnglishNameFinder(mModelPath + "NameFind\")
        End If
        Dim models() As String = {"date", "location", "money", "organization", "percentage", "person", "time"}
        FindNames = mNameFinder.GetNames(models, sentence)
    End Function

    Public Function FindDateTime(ByVal sentence As String) As String
        If mNameFinder Is Nothing Then
            mNameFinder = New OpenNLP.Tools.NameFind.EnglishNameFinder(mModelPath + "NameFind\")
        End If
        Dim models() As String = {"date", "time"}
        FindDateTime = mNameFinder.GetNames(models, sentence)
    End Function

End Class
