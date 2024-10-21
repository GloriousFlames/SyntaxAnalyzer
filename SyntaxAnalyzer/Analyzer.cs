using System.Text.RegularExpressions;

namespace SyntaxAnalyzer
{
    internal static class Analyzer
    {
        private const int MAX_ID_LENGTH = 8;
        private const int MIN_INT = -255;
        private const int MAX_INT = 255;
        private static readonly string[] RESERVED_WORDS = [
            "VAR", "FILE", "OF", "TEXT", "FILE", "CHAR", "STRING", "DOUBLE", "SINGLE", "BYTE", "REAL", "INTEGER"];
        
        private enum States {Start, AfterVAR, BeforeID, AfterID, BeforeFile, AfterFile, Final, Error}

        public static bool Analyze(string st)
        {
            States state = States.Start;
            string str = Regex.Replace(st.Trim(), "\\s+", " ");
            int pos = 0;
            switch (state)
            {
                case States.Start:
                    if (str.StartsWith("VAR")) { state = States.AfterVAR; }
                    else { 
                        state = States.Error; 
                        
                    }
                    break;
            }
            return false;
        }
    }
}
