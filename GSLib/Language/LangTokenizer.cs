using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Language
{
    public class LangTokenizer
    {
        bool inString = false;
        char openString = '\0';
        bool inEscape = false;
        bool inComment = false;
        int openComment = 0;


        char[] stringOp;
        char escape;
        char[][] escSeq;
        char[] eot;
        char[] ops;
        string[] dops;
        string[] singleComment;
        string[] multiCommentOpen;
        string[] multiCommentClose;

        bool initialized = false;
        public bool Initialized
        {
            get
            {
                return initialized;
            }
        }

        public LangTokenizer(char[] stringOperators, char escapeChar, char[][] escSequences, char[] endOfToken, char[] operators,
                         string[] doubleOperators, string[] singleComm, string[] multiCommOpen, string[] multiCommClose)
        {
            stringOp = stringOperators;
            escape = escapeChar;
            escSeq = escSequences;
            eot = endOfToken;
            ops = operators;
            dops = doubleOperators;
            singleComment = singleComm;
            multiCommentOpen = multiCommOpen;
            multiCommentClose = multiCommClose;

            initialized = true;
        }

        private string[] GetTokensFromLine(string line, int lineID)
        {
            List<char> lineChars = new List<char>();
            List<char> token = new List<char>();
            List<string> tokens = new List<string>();

            Dictionary<char, char> esc = new Dictionary<char, char>();

            AddRangeToDictionary<char>(ref esc, escSeq);

            lineChars.AddRange(line);

            // build tokens
            foreach (char c in lineChars)
            {
                if (!inComment)
                {
                    if (stringOp.Contains(c))   // if the token is a string operator
                    {
                        if (inString)                   // if we're in a string right now
                        {
                            if (openString == c)        // if the string operator used is matches the current char
                            {
                                inString = false;               // exit the string
                                openString = '\0';              // reset the string opener
                                token.Add(c);
                                tokens.Add(MakeToken(token.ToArray()));
                                token.Clear();
                            }
                            else
                            {
                                token.Add(c);
                            }
                        }
                        else
                        {
                            openString = c;
                            inString = true;
                            if (token.Count > 0)
                            {
                                tokens.Add(MakeToken(token.ToArray()));
                                token.Clear();
                            }
                            token.Add(c);
                        }
                    }
                    else if (!inString)
                    {
                        if (eot.Contains(c))
                        {
                            if (token.Count > 0)
                            {
                                tokens.Add(MakeToken(token.ToArray()));
                                token.Clear();
                            }
                        }
                        else if (ops.Contains(c))
                        {
                            string t = MakeToken(token.ToArray());
                            token.Clear();
                            if (!dops.Contains(t + c))
                            {
                                if (multiCommentOpen.Contains(t + c))
                                {
                                    for (int i = 0; i < multiCommentOpen.Length; i++)
                                    {
                                        if (multiCommentOpen[i] == t + c)
                                        {
                                            openComment = i;
                                            inComment = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (t != "")
                                    {
                                        tokens.Add(t);
                                    }
                                    token.Add(c);
                                }
                            }
                            else
                            {
                                tokens.Add(t + c);
                            }
                        }
                        else
                        {
                            if (token.Count > 0 && ops.Contains(token[0]))
                            {
                                tokens.Add(MakeToken(token.ToArray()));
                                token.Clear();
                            }
                            token.Add(c);
                        }
                    }
                    else
                    {
                        if (inEscape)
                        {
                            token.Add(esc[c]);
                            inEscape = false;
                        }
                        else if (c == escape)
                        {
                            inEscape = true;
                        }
                        else
                        {
                            token.Add(c);
                        }
                    }
                }
                else
                {
                    token.Add(c);
                    if (token.Count == multiCommentClose[openComment].Length)
                    {
                        string t = MakeToken(token.ToArray());
                        token.Clear();
                        if (multiCommentClose[openComment] == t)
                        {
                            inComment = false;
                        }
                        else
                        {
                            token.Add(c);
                        }
                    }
                    else if (token.Count > multiCommentClose[openComment].Length)
                    {
                        token.Clear();
                    }
                }
            }

            if (token.Count > 0 & !inComment)
            {
                tokens.Add(MakeToken(token.ToArray()));
                token.Clear();
            }
            else if (inComment)
            {
                token.Clear();
                tokens.Clear();
            }

            return tokens.ToArray();
        }

        public string[] GetTokens(string[] lines)
        {
            if (!initialized)
            {
                return new string[] { };
            }

            List<string> tokens = new List<string>();

            // list out all chars
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                foreach (string commenter in singleComment) // look for single-line comments and remove them
                {
                    if (line.Contains(commenter))
                    {
                        line = line.Remove(line.IndexOf(commenter));
                        break;
                    }
                }
                tokens.AddRange(GetTokensFromLine(line, i + 1));
            }

            return tokens.ToArray();
        }

        private string MakeToken(char[] chars)
        {
            string token = "";
            foreach (char c in chars)
            {
                token = token + c;
            }
            return token;
        }

        private void AddRangeToDictionary<T>(ref Dictionary<T, T> dict, T[][] values)
        {
            T[] root = values[0];
            T[] map = values[1];

            for (int i = 0; i < root.Length; i++)
            {
                dict.Add(root[i], map[i]);
            }
        }
    }
}
