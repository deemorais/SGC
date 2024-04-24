using SGC;
using SGC.Models;

public class Program
{
    static void Main()
    {
        try
        {
            string opt = "1";
            string[] opts = ["1", "2", "3"];

            while (opts.Contains(opt))
            {
                IList<string> menu = new List<string>(new string[] {
                "Informe a opção que deseja utilizar",
                "1 - Gerar automaticamente as trilhas a partir das opções enviadas no e-email",
                "2 - Incluir manualmente as trilhas",
                "3 - Limpar console",
                "0 - Sair" });
                opt = Utils.MontarCabecalho(menu, "A opção digitada precisa ser um número inteiro e estar entre as opções acima").ToString();
                Console.Clear();

                switch (opt)
                {
                    case "1":
                        Utils.ShowTrials(Lecture.GetList());
                        break;
                    case "2":
                        Utils.ShowTrials(Lecture.AddLectures());
                        break;
                    case "3": Console.Clear(); break;
                    default: break;
                }
            }
        }
        catch (Exception)
        {
        }
    }
}
