using SGC.Models;

namespace SGC
{
    public class Utils
    {
        public static int MontarCabecalho(IList<string> Pergunta, string Erro)
        {
            try
            {
                string opt = string.Empty;

                int qtd = 0;
                while (opt == string.Empty)
                {

                    foreach (string s in Pergunta)
                    {
                        Console.WriteLine(s);
                    }
                    opt = Console.ReadLine();

                    try
                    {
                        qtd = int.Parse(opt);
                    }
                    catch
                    {
                        throw new Exception(Erro);
                    }

                }
                return qtd;
            }
            catch (Exception ex)
            {
                IList<string> message = new List<string>(new string[] {
                    "Encontramos um problema:",
                    ex.Message
                });
                MensagemConsole(message);

                throw ex;
            }
        }

        public static void MensagemConsole(IList<string> Mensagem)
        {
            foreach (var item in Mensagem)
            {
                Console.WriteLine("");
                Console.WriteLine(item);
                Console.WriteLine("");
            }
        }

        public static void ShowTrials(IList<Lecture> Lectures)
        {
            try
            {
                int qtd = MontarCabecalho(new List<string> { "Informe a quantidade de Trilhas que deseja obter" }, "A Quantidade de trilhas deve ser um número inteiro");
                Console.Clear();

                for (int i = 0; i < qtd; i++)
                {
                    MensagemConsole(new List<string> { string.Concat("Trilha nº", i + 1) });

                    //List<Lecture> LecturesManipulate = new List<Lecture>();
                    //LecturesManipulate.AddRange(Lecture.GenerateList(Lectures));
                    ShowLectures(Lecture.GenerateList(Lectures));

                    MensagemConsole(new List<string> { "---------------------------------------------------------------------------------------" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void ShowLectures(IList<Lecture> Lectures)
        {
            try
            {
                DateTime realTime = DateTime.MinValue.AddHours(9);

                foreach (var l in Lectures)
                {
                    Console.WriteLine(string.Concat(
                                                       realTime.ToString("HH:mm"), " às ", realTime.AddMinutes(l.Time.TotalMinutes).ToString("HH:mm")
                                                       , "   |   Tema: ", l.Name
                                                       , "   |   Tempo total: ", l.Time));

                    realTime = realTime.AddMinutes(l.Time.TotalMinutes);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
