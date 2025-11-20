using System.ComponentModel;
using System.Text.RegularExpressions;

namespace AgendaNet_Domain.Utilities
{
    public static class DataValidations
    {
        public static bool IsValidURL(string url)
        {
            string Pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(url);
        }
        public static bool IsValidColor(string color)
        {
            return Regex.IsMatch(color, @"[#][0-9A-Fa-f]{6}\b");
        }
        public static bool IsValidEmail(string address)
        {
            return Regex.IsMatch(address, "(?<user>[^@]+)@(?<host>.+)");
        }
        public static bool IsValidState(string state)
        {
            const string CONST_LISTA_UF = "AC;AL;AM;AP;BA;CE;DF;ES;GO;MA;MG;MS;MT;PA;PB;PE;PI;PR;RJ;RN;RO;RR;RS;SC;SE;SP;TO";

            if (state.Length < 2)
                return false;

            if (CONST_LISTA_UF.Contains(state) == false)
                return false;

            return true;
        }
        private static bool IsValidTime(int time)
        {
            int hours = time / 100;
            int minutes = time % 100;

            return hours >= 0 && hours <= 23 && minutes >= 0 && minutes <= 59;
        }
        public static bool IsValidCPFCNPJ(string number)
        {
            if (number.Length == 11)
            {
                return valida_cpf(number);
            }
            if (number.Length == 14)
            {
                return valida_cnpj(number);
            }
            else
            {
                return false;
            }
        }

        private static bool valida_cpf(string value)
        {
            string _numbercpf = value.Replace(".", "");

            _numbercpf = _numbercpf.Replace("-", "");

            if (_numbercpf.Length != 11)
            {
                return false;
            }

            bool _igual = true;

            for (int i = 1; i < 11 && _igual; i++)
            {
                if (_numbercpf[i] != _numbercpf[0])
                {
                    _igual = false;
                }
            }

            if (_igual || _numbercpf == "12345678909")
            {
                return false;
            }
            else if (_igual || _numbercpf == "00000000000")
            {
                return true;
            }

            int[] _number = new int[11];

            for (int i = 0; i < 11; i++)
            {
                _number[i] = int.Parse(_numbercpf[i].ToString());
            }

            int _sum = 0;

            for (int i = 0; i < 9; i++)
            {
                _sum += (10 - i) * _number[i];
            }

            int _result = _sum % 11;
            if (_result == 1 || _result == 0)
            {
                if (_number[9] != 0)
                {
                    return false;
                }
            }
            else if (_number[9] != 11 - _result)
            {
                return false;
            }

            _sum = 0;
            for (int i = 0; i < 10; i++)
            {
                _sum += (11 - i) * _number[i];
            }

            _result = _sum % 11;

            if (_result == 1 || _result == 0)
            {
                if (_number[10] != 0)
                {
                    return false;
                }
            }
            else
            {
                if (_number[10] != 11 - _result)
                {
                    return false;
                }
            }
            return true;
        }
        private static bool valida_cnpj(string value)
        {
            string _numbercnpj = value.Replace(".", "");
            _numbercnpj = _numbercnpj.Replace("/", "");
            _numbercnpj = _numbercnpj.Replace("-", "");

            int[] _digits, sum, _result;

            int nrdig;
            string ftmt;
            bool[] cnpjok;

            ftmt = "6543298765432";

            _digits = new int[14];

            sum = new int[2];
            sum[0] = 0;
            sum[1] = 0;

            _result = new int[2];
            _result[0] = 0;
            _result[1] = 0;

            cnpjok = new bool[2];
            cnpjok[0] = false;
            cnpjok[1] = false;


            try
            {

                for (nrdig = 0; nrdig < 14; nrdig++)
                {
                    _digits[nrdig] = int.Parse(_numbercnpj.Substring(nrdig, 1));

                    if (nrdig <= 11)
                        sum[0] += _digits[nrdig] * int.Parse(ftmt.Substring(nrdig + 1, 1));

                    if (nrdig <= 12)
                        sum[1] += _digits[nrdig] * int.Parse(ftmt.Substring(nrdig, 1));
                }


                for (nrdig = 0; nrdig < 2; nrdig++)
                {
                    _result[nrdig] = sum[nrdig] % 11;

                    if (_result[nrdig] == 0 || _result[nrdig] == 1)
                        cnpjok[nrdig] = _digits[12 + nrdig] == 0;
                    else
                        cnpjok[nrdig] = _digits[12 + nrdig] == 11 - _result[nrdig];
                }

                return cnpjok[0] && cnpjok[1];
            }
            catch
            {
                return false;
            }
        }


        #region validatepassword
        public static password_strength CheckPasswordStrength(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return password_strength.Unacceptable;

            int _score = 0;

            //comprimento mínimo
            if (password.Length >= 8)
                _score++;

            //letra maiúscula
            if (Regex.IsMatch(password, "[A-Z]"))
                _score++;

            //letra minúscula
            if (Regex.IsMatch(password, "[a-z]"))
                _score++;

            //número
            if (Regex.IsMatch(password, "[0-9]"))
                _score++;

            //caractere especial
            if (Regex.IsMatch(password, "[!@#$%^&*(),.?\":{}|<>]"))
                _score++;

            // Classificação baseada na pontuação
            return _score switch
            {
                <= 3 => password_strength.Weak,
                4 => password_strength.Acceptable,
                5 => password_strength.Strong,
                _ => password_strength.Unacceptable,
            };
        }
        #endregion
        public enum password_strength
        {
            [Description("Inaceitavel")] Unacceptable,
            [Description("Fraca")] Weak,
            [Description("Aceitavel")] Acceptable,
            [Description("Forte")] Strong,
            [Description("Segura")] Safe
        }
    }
}
