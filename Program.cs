using System;

namespace desarrollo_prueba_parcial
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declaring numbers
            string n1 = "209384967394853864273864174981097423097409384109748184729386513692865983472893473743986598475297348192491";
            string n2 = "389274293874928365289572934029340293742398742386578256298374298374298374298374928374289374239856223423897";

            // Page to test the operations: https://www.boxentriq.com/code-breaking/big-number-calculator

            // Make operations
            string suma = prepararSuma(n1, n2);
            string resta = prepararResta(n1, n2);
            string multiplicacion = prepararMultiplicacion(n1, n2);

            // Results
            Console.WriteLine($"N1: {n1}\nN2: {n2}\n");

            Console.WriteLine($"Suma: {suma}");
            Console.WriteLine($"Rest: {resta}");
            Console.WriteLine($"Mult: {multiplicacion}");
        }

        // MULTIPLICACION
        static string prepararMultiplicacion(string n1, string n2)
        {

            if (n1.Length > n2.Length)
            {
                n2 = igualarStrings(n2, n1.Length);
            }
            else if (n2.Length > n1.Length)
            {
                n1 = igualarStrings(n1, n2.Length);
            }

            return multiplicar(n1, n2);
        }

        static string multiplicar(string n1, string n2)
        {
            string resultado = "";

            char[] ch1 = n1.ToCharArray();
            Array.Reverse(ch1);
            n1 = new string(ch1);

            char[] ch2 = n2.ToCharArray();
            Array.Reverse(ch2);
            n2 = new string(ch2);

            string[] res = new string[n1.Length];

            for (int i = 0; i < n2.Length; i++)
            {
                int acarreo = 0;
                string m = new string('0', i);
                for (int j = 0; j < n1.Length; j++)
                {
                    int r = acarreo + (Convert.ToInt16(n2[i] + " ") * Convert.ToInt16(n1[j] + " "));
                    m += r % 10;
                    acarreo = (r.ToString().Length >= 2) ? Convert.ToInt16(r.ToString()[0] + " ") : 0;
                }

                m += (acarreo != 0) ? acarreo.ToString() : "";

                char[] ch_m = m.ToCharArray();
                Array.Reverse(ch_m);
                m = new string(ch_m);

                res[i] = m;
            }

            for (int i = 0; i < res.Length; i++)
            {
                resultado = prepararSuma(resultado, res[i]);
            }

            return resultado;
        }

        // RESTA
        static bool esMenor(string str1, string str2)
        {
            int n1 = str1.Length, n2 = str2.Length;

            if (n1 < n2)
                return true;
            if (n2 < n1)
                return false;

            for (int i = 0; i < n1; i++)
            {
                if (str1[i] < str2[i])
                    return true;
                else if (str1[i] > str2[i])
                    return false;
            }

            return false;
        }

        static string prepararResta(string n1, string n2)
        {
            if (n1.Length > n2.Length)
            {
                n2 = igualarStrings(n2, n1.Length);
            }
            else if (n2.Length > n1.Length)
            {
                n1 = igualarStrings(n1, n2.Length);
            }

            if (esMenor(n1, n2))
            {
                string aux = n1;
                n1 = n2;
                n2 = aux;
            }

            return restar(n1, n2);
        }

        static string restar(string str1, string str2)
        {
            string resultado = "";

            int n1 = str1.Length;
            int n2 = str2.Length;

            char[] ch1 = str1.ToCharArray();
            Array.Reverse(ch1);
            str1 = new string(ch1);

            char[] ch2 = str2.ToCharArray();
            Array.Reverse(ch2);
            str2 = new string(ch2);

            int acarreo = 0;
            int i = 0;

            do
            {
                int sub = ((int)(str1[i] - '0') - (int)(str2[i] - '0') - acarreo);

                if (sub < 0)
                {
                    sub = sub + 10;
                    acarreo = 1;
                }
                else
                    acarreo = 0;

                resultado += (char)(sub + '0');
                i++;

            } while (i < n2);

            i = n2;
            do
            {
                int sub = ((int)(str1[i - 1] - '0') - acarreo);
                if (sub < 0)
                {
                    sub = sub + 10;
                    acarreo = 1;
                }
                else
                    acarreo = 0;
                resultado += (char)(sub + '0');
                i++;
            } while (i < n1);

            string aux = "";
            i = resultado.Length - 2;
            do
            {
                aux += resultado[i];
                i--;
            } while (i >= 0);
            return aux;
        }

        // SUMA
        private static string prepararSuma(string n1, string n2)
        {
            if (n1.Length > n2.Length)
            {
                n2 = igualarStrings(n2, n1.Length);
            }
            else if (n2.Length > n1.Length)
            {
                n1 = igualarStrings(n1, n2.Length);
            }

            return sumar(n1, n2);
        }

        private static string igualarStrings(string n, int tam)
        {
            return new string('0', tam - n.Length) + n;
        }

        private static string sumar(string n1, string n2)
        {
            string suma = "";
            int llevando = 0;

            for (int i = n1.Length - 1; i >= 0; i--)
            {

                int s = Convert.ToInt32(n1[i] + "") + Convert.ToInt32(n2[i] + "") + llevando;

                int f = 0;

                if (s >= 10)
                {
                    f = Convert.ToInt32(s.ToString()[1] + " ");
                    llevando = Convert.ToInt32(s.ToString()[0] + " ");
                }
                else
                {
                    llevando = 0;
                    f = s;
                };

                suma += f;
            }

            suma += (llevando == 0) ? "" : llevando.ToString();

            char[] ch1 = suma.ToCharArray();
            Array.Reverse(ch1);

            suma = new string(ch1);

            return eliminarCerosIzquierda(suma);
        }

        static string eliminarCerosIzquierda(string resultado)
        {
            int n = 0;

            for (int i = 0; i < resultado.Length; i++)
            {
                if (resultado[i].Equals('0'))
                {
                    n++;
                }
                else { break; }
            }
            return resultado.Substring(n);
        }

    }
}

