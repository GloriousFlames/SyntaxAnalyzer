﻿using System.Text.RegularExpressions;

namespace SyntaxAnalyzer
{
    public partial class MainAnalyzer : Form
    {
        public MainAnalyzer()
        {
            InitializeComponent();
        }

        private static readonly string[] RESERVED_WORDS = 
            ["var", "file", "of", "text", "file", "char", "string", "double", "single", "byte", "real", "integer"];
        private static readonly string[] TYPES =
            ["char", "string", "integer", "real", "double", "single", "byte"];

        Dictionary<String,String> IDs = [];
        private enum States { Start, AfterVAR, BeforeID, AfterID, BeforeFile, AfterFile, Final, Error }

        private void ButtonSemantic_Click(object sender, EventArgs e)
        {
            tbSemantic.Text = "Список идентификаторов:\r\n";
            foreach (var ID in IDs.OrderBy(ID => ID.Key))
            {
                tbSemantic.Text += $"{ID.Key}: {ID.Value}\r\n";
            }
            btnSemantic.Enabled = false;
        }

        private void ButtonSyntax_Click(object sender, EventArgs e)
        {
            if (Analyze(rtbInput.Text))
            {
                btnSemantic.Enabled = true;
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Написать программу синтаксического анализа автоматного языка с включением " +
                "семантики,  фрагмента оператора описания файлов языка Turbo Pascal, имеющего вид:\r\n\r\n" +
                "VAR <список описаний>;\r\n<список описаний> :: =<описание>[,<описание>]\r\n<описание>:: " +
                "=<список идентификаторов>:<описание файла>\r\n<список идентификаторов>:: =<идентификатор>" +
                "[,<идентификатор>]\r\n<описание файла> :: = FILE OF<тип>|TEXT|FILE\r\n<тип> :: = " +
                "CHAR|STRING(<целая константа>)|INTEGER|REAL|DOUBLE|SINGLE|BYTE\r\n\r\n<идентификатор> - " +
                "идентификатор языка начинается с буквы, включает совокупность букв, цифр, не допускает " +
                "пробелы и специальные символы, \r\n<целая константа> - целое число без знака;\r\n\r\n" +
                "Семантика:\r\nИдентификатор имеет ограничение на длину (не более 8 символов) и не может быть" +
                " зарезервированным словом - VAR, FILE, OF, TEXT, FILE, CHAR, STRING, DOUBLE, SINGLE, BYTE, " +
                "REAL, INTEGER.\r\nЦелая константа - число в диапазоне  0  255;\r\nПостроить и вывести на " +
                "печать упорядоченный список идентификаторов. Не допускать повторное описание идентификаторов" +
                " файлов.\r\nСообщать об ошибках при анализе, указывая курсором место возникновения ошибки и " +
                "ее содержание.\r\nАнализатор работает до первой ошибки, допускает произвольное число " +
                "пробелов между конструкциями оператора (пробелы могут и отсутствовать), не учитывается " +
                "регистр символов.\r\n \r\nПримеры правильных цепочек:\r\n\r\nVAR  name : file  of  char, " +
                "AB : file  of  STRING (10), A2C : TEXT;\r\n\r\nVAR  C12, S1 , e : file  of  Integer, D : " +
                "File;\r\n \r\n", "Задание");
        }

        private bool Analyze(string st)
        {
            States state = States.Start;
            string str = Regex.Replace(st.Trim(), "\\s+", " ").ToLower();
            int pos = 0;
            IDs = [];
            string errMessage = "";
            bool isBelong = false;

            if (str.Length == 0)
            {
                rtbSyntax.Text = "Синтаксическая ошибка!\r\nОператор должен иметь вид:\r\nVAR <список идентификаторов> : <описание файла>;";
                return isBelong;
            }

            while (state != States.Final && state != States.Error)
            {
                if (pos >= str.Length)
                {
                    pos = str.Length - 1;
                    errMessage = "Синтаксическая ошибка!\r\nОператор должен иметь вид:\r\nVAR <список идентификаторов> : <описание файла>;";
                    state = States.Error;
                }
                switch (state)
                {
                    case States.Start:
                        if (str.StartsWith("var"))
                        {
                            state = States.AfterVAR;
                            pos += 3;
                        }
                        else
                        {
                            state = States.Error;
                            errMessage = "Синтаксическая ошибка!\r\nОператор должен начинаться с ключевого слова VAR!";
                            goto case States.Error;
                        }
                        break;

                    case States.AfterVAR:
                        if (str[pos] == ' ')
                        {
                            state = States.BeforeID;
                            pos++;
                        }
                        else
                        {
                            state = States.Error;
                            errMessage = "Синтаксическая ошибка!\r\nПосле ключевого слова VAR должен стоять пробел!";
                            goto case States.Error;
                        }
                        break;

                    case States.BeforeID:
                        string ID = "";
                        bool isReserved = false;

                        if (str[pos] == ' ') pos++;

                        if (str[pos] >= 'a' && str[pos] <= 'z')
                        {
                            ID += str[pos];
                            pos++;
                            while (pos < str.Length && (str[pos] >= 'a' && str[pos] <= 'z' || str[pos] >= '0' && str[pos] <= '9'))
                            {
                                ID += str[pos];
                                pos++;
                            }
                            foreach (string id in RESERVED_WORDS)
                            {
                                if (ID == id)
                                {
                                    isReserved = true;
                                    break;
                                }
                            }
                            if (IDs.ContainsKey(ID))
                            {
                                errMessage = "Семантическая ошибка!\r\nID не должны повторяться!";
                                state = States.Error;
                                goto case States.Error;
                            }
                            else if (isReserved)
                            {
                                errMessage = "Семантическая ошибка!\r\nID не должен быть зарезервированным словом!";
                                state = States.Error;
                                goto case States.Error;
                            }
                            else if (ID.Length > 8)
                            {
                                errMessage = "Семантическая ошибка!\r\nДлина ID должна быть не больше 8!";
                                state = States.Error;
                                goto case States.Error;
                            }
                            else
                            {
                                IDs.Add(ID,"");
                                state = States.AfterID;
                            }
                        }
                        else
                        {
                            errMessage = "Синтаксическая ошибка!\r\nID должен начинаться с буквы!";
                            state = States.Error;
                            goto case States.Error;
                        }
                        break;

                    case States.AfterID:
                        if (str[pos] == ' ') pos++;

                        if (str[pos] == ':')
                        {
                            state = States.BeforeFile;
                            pos++;
                        }
                        else if (str[pos] == ',')
                        {
                            state = States.BeforeID;
                            pos++;
                        }
                        else
                        {
                            errMessage = "Синтаксическая ошибка!\r\nПосле ID может быть только запятая или двоеточие!";
                            state = States.Error;
                            goto case States.Error;
                        }
                        break;

                    case States.BeforeFile:
                        if (str[pos] == ' ') pos++;

                        if (pos + 7 <= str.Length && str.Substring(pos, 7) == "file of")
                        {
                            pos += 7;
                            bool typeFound = false;
                            if (str[pos] == ' ') pos++;

                            foreach (string type in TYPES)
                            {
                                if (pos + type.Length < str.Length && str.Substring(pos, type.Length) == type)
                                {
                                    typeFound = true;
                                    pos += type.Length;
                                    if (type == "string")
                                    {
                                        int openIndex, closeIndex; string number = "";
                                        if (str[pos] == ' ') pos++;

                                        if (str.Contains('(') && str.Contains(')'))
                                        {
                                            openIndex = str.IndexOf('(', pos);
                                            closeIndex = str.IndexOf(')', pos);
                                            if (int.TryParse(str.Substring(openIndex + 1, closeIndex - openIndex - 1), out _))
                                            {
                                                number = str.Substring(openIndex + 1, closeIndex - openIndex - 1);
                                            }
                                            else
                                            {
                                                errMessage = "Синтаксическая ошибка!\r\nТип string требует наличия целой константы в скобках!";
                                                state = States.Error;
                                                goto case States.Error;
                                            }
                                        }
                                        else
                                        {
                                            errMessage = "Синтаксическая ошибка!\r\nТип string требует наличия целой константы в скобках!";
                                            state = States.Error;
                                            goto case States.Error;
                                        }
                                        if (int.Parse(number) >= 0 && int.Parse(number) <= 255)
                                        {
                                            foreach (string value in IDs.Keys)
                                            {
                                                if (IDs[value] == "") IDs[value] = $"file of {type}";

                                            }
                                            state = States.AfterFile;
                                            pos += 2 + number.ToString().Length;
                                            break;
                                        }
                                        else
                                        {
                                            errMessage = "Семантическая ошибка!\r\nЧисло должно лежать в отрезке [0;255]!";
                                            state = States.Error;
                                            goto case States.Error;
                                        }
                                    }
                                    state = States.AfterFile;
                                }
                            }
                            if (!typeFound)
                            {
                                errMessage = "Синтаксическая ошибка!\r\nОписание файла должно содержать тип!";
                                state = States.Error;
                                goto case States.Error;
                            }
                        }
                        else if (pos + 4 <= str.Length && (str.Substring(pos, 4) == "text" || str.Substring(pos, 4) == "file"))
                        {
                            foreach (string value in IDs.Keys)
                            {
                                if (IDs[value] == "") IDs[value] = str.Substring(pos, 4);

                            }
                            pos += 4;
                            state = States.AfterFile;
                        }
                        else
                        {
                            errMessage = "Синтаксическая ошибка!\r\nОписание файла должно содержать TEXT, FILE или FILE OF!";
                            state = States.Error;
                            goto case States.Error;
                        }
                        break;

                    case States.AfterFile:
                        if (str[pos] == ' ') pos++;

                        if (str[pos] == ',')
                        {
                            state = States.BeforeID;
                            pos++;
                        }
                        else if (str[pos] == ';')
                        {
                            state = States.Final;
                            goto case States.Final;
                        }
                        else
                        {
                            errMessage = "Синтаксическая ошибка!\r\nПосле описания файла может быть только запятая или точка с запятой!";
                            state = States.Error;
                            goto case States.Error;
                        }
                        break;

                    case States.Final:
                        isBelong = true;
                        rtbSyntax.Text = "Цепочка принадлежит языку";
                        rtbSyntax.ForeColor = Color.Black;
                        rtbInput.Text = str;
                        rtbInput.SelectAll();
                        rtbInput.SelectionColor = Color.Black;
                        break;

                    case States.Error:
                        rtbSyntax.Text = errMessage;
                        rtbSyntax.ForeColor = Color.Red;
                        rtbInput.Text = str;
                        rtbInput.Select(pos, 1);
                        int errPos = TextRenderer.MeasureText(str.Substring(0, pos + 1), Font).Width - 10;
                        Cursor.Position = new Point
                            (Location.X + 8 + rtbInput.Location.X + errPos,
                            Location.Y + rtbInput.Location.Y + rtbInput.Height + 30);
                        rtbInput.SelectionColor = Color.Red;
                        break;
                    }
            }
            return isBelong;
        }
    }
}
