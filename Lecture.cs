using System.Text.RegularExpressions;


namespace SGC.Models
{
    public class Lecture(string FullName) // Palestras
    {
        public string FullName { get; set; } = FullName.TrimEnd();
        public string Name { get; private set; } = GetNameOnly(FullName);
        public TimeSpan Time { get; private set; } = GetTimeOnly(FullName);

        #region private

        private static string GetNameOnly(string FullName)
        {
            try
            {
                if (string.IsNullOrEmpty(FullName))
                    throw new Exception("Por favor informe o tema e o tempo de duração da palestra");

                MatchCollection matches = Regex.Matches(FullName, " ");
                if (matches.Count > 0)
                    return FullName.Substring(0, matches.Last().Index);
                else
                    throw new Exception("O Tema e Tempo de duração informados não são válidos");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private static TimeSpan GetTimeOnly(string FullName)
        {
            try
            {
                if (FullName.ToLower().EndsWith("relâmpago"))
                    return TimeSpan.FromMinutes(5);
                else
                {
                    MatchCollection matches = Regex.Matches(FullName, " ");

                    if (matches.Count > 0)
                    {
                        string s = FullName.ToLower()
                                       .Substring(matches.Last().Index + 1)
                                       .Replace("min", "");
                        TimeSpan ts = TimeSpan.FromMinutes(Convert.ToInt32(s));

                        if (ts.TotalMinutes > 60)
                            throw new Exception("A Palestra não pode ter mais de 60min");

                        return ts;
                    }
                    else
                    {
                        throw new Exception("Não foi encontrado um tempo de duração válido na palestra informada.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region public

        public static IList<Lecture> GetList()
        {
            IList<Lecture> list = new List<Lecture>();

            list.Add(new Lecture("Writing Fast Tests Against Enterprise.Net 60min"));
            list.Add(new Lecture("Overdoing it in Python 45min"));
            list.Add(new Lecture("Lua for the Masses 30min"));
            list.Add(new Lecture(".Net Errors from Mismatched Nuget Versions 45min"));
            list.Add(new Lecture("Common.Net Errors 45min"));
            list.Add(new Lecture("Python for .Net Developers relâmpago"));
            list.Add(new Lecture("Communicating Over Distance 60min"));
            list.Add(new Lecture("Accounting - Driven Development 45min"));
            list.Add(new Lecture("Woah 30min"));
            list.Add(new Lecture("Sit Down and Write 30min"));
            list.Add(new Lecture("Pair Programming vs Noise 45min"));
            list.Add(new Lecture(".Net Magic 60min"));
            list.Add(new Lecture(".Net Core: Why We Should Move On 60min"));
            list.Add(new Lecture("Clojure Ate Scala(on my project) 45min"));
            list.Add(new Lecture("Programming in the Boondocks of Seattle 30min"));
            list.Add(new Lecture(".Net vs.Clojure for Back - End Development 30min"));
            list.Add(new Lecture(".Net Core Legacy App Maintenance 60min"));
            list.Add(new Lecture("A World Without HackerNews 30min"));
            list.Add(new Lecture("User Interface CSS in .Net Apps 30min"));

            return list;
        }

        public static IList<Lecture> GenerateList(IList<Lecture> Lectures)
        {
            bool trailIsCompleted = false;

            DateTime realTime = DateTime.MinValue.AddHours(9);
            DateTime initialLunch = DateTime.MinValue.AddHours(12);
            DateTime initialNetworking = DateTime.MinValue.AddHours(16);
            DateTime finalNetworking = DateTime.MinValue.AddHours(17);

            Lecture addLecture = null;
            Lecture lunch = new Lecture("Almoço 60min");
            Lecture networking = new Lecture("Networking Event 60min");

            List<Lecture> LecturesManipulate = new List<Lecture>();
            LecturesManipulate.AddRange(Lectures);

            IList<Lecture> returnLectures = new List<Lecture>();

            Random rnd = new Random();

            try
            {
                while (!trailIsCompleted)
                {
                    int r = rnd.Next(LecturesManipulate.Count);
                    double minutesForLunch = (realTime - initialLunch).TotalMinutes;

                    if (LecturesManipulate.Count > 0 && !returnLectures.Contains(LecturesManipulate[r]))
                    {
                        addLecture = (LecturesManipulate[r]);

                        if (minutesForLunch >= 0 && minutesForLunch < 60)
                        {
                            if (minutesForLunch == 0)
                            {
                                addLecture = (lunch);
                            }
                            else
                            {
                                LecturesManipulate.Add(returnLectures[returnLectures.Count - 1]);
                                realTime = realTime.AddMinutes(-(returnLectures[returnLectures.Count - 1].Time.TotalMinutes));
                                returnLectures.RemoveAt(returnLectures.Count - 1);

                                double emptyTime = (initialLunch - realTime).TotalMinutes;

                                IList<Lecture> l = LecturesManipulate.Where(l => l.Time.TotalMinutes <= emptyTime && !returnLectures.Contains(l)).OrderBy(l => l.Time.TotalMinutes).ToList();

                                if (l.Count > 0)
                                    addLecture = l.LastOrDefault();
                                else
                                    addLecture = (new Lecture(string.Concat("Tempo livre ", emptyTime, "min")));

                            }
                        }
                        else if (realTime >= initialNetworking && realTime <= finalNetworking)
                        {
                            addLecture = (networking);
                            trailIsCompleted = true;
                        }

                        returnLectures.Add(addLecture);
                        realTime = realTime.AddMinutes(returnLectures[returnLectures.Count - 1].Time.TotalMinutes);
                        LecturesManipulate.Remove(addLecture);
                    }
                    else if (LecturesManipulate.Count == 0)
                    {
                        if (!returnLectures.Contains(lunch))
                        {
                            double emptyTime = (initialLunch - realTime).TotalMinutes;

                            if (emptyTime > 0)
                            {
                                returnLectures.Add(new Lecture(string.Concat("Tempo livre ", (emptyTime > 60 ? 60 : emptyTime), "min")));
                                realTime = realTime.AddMinutes(returnLectures[returnLectures.Count - 1].Time.TotalMinutes);
                            }
                            else
                            {
                                returnLectures.Add(lunch);
                                realTime = realTime.AddMinutes(returnLectures[returnLectures.Count - 1].Time.TotalMinutes);
                            }
                        }
                        else if (!returnLectures.Contains(networking))
                        {
                            double emptyTime = (initialNetworking - realTime).TotalMinutes;
                            if (emptyTime > 0)
                            {
                                returnLectures.Add(new Lecture(string.Concat("Tempo livre ", (emptyTime > 60 ? 60 : emptyTime), "min")));
                                realTime = realTime.AddMinutes(returnLectures[returnLectures.Count - 1].Time.TotalMinutes);
                            }
                            else
                            {
                                returnLectures.Add(networking);
                                realTime = realTime.AddMinutes(returnLectures[returnLectures.Count - 1].Time.TotalMinutes);
                            }
                        }
                        else
                        {
                            trailIsCompleted = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return returnLectures;
        }

        public static IList<Lecture> AddLectures()
        {
            IList<Lecture> Lectures = new List<Lecture>();

            try
            {
                int qtd = Utils.MontarCabecalho(new List<string> { "Informe a quantidade de palestras que deseja incluir" }, "A Quantidade de palestras deve ser um número inteiro");

                while (qtd > Lectures.Count)
                {
                    Console.WriteLine(qtd == 0 ?
                                        "Informe o tema e o tempo de duração para cada palestra, conforme exemplo abaixo:"
                                        : "Informe o tema e o tempo de duração para cada palestra:");
                    if (qtd == 0)
                        Console.WriteLine("Writing Fast Tests Against Enterprise.Net 60min");

                    Lecture Lecture = new Lecture(Console.ReadLine());

                    if (Lectures.Where(l => l.FullName == Lecture.FullName).ToList().Count > 0)
                    {
                        Utils.MensagemConsole(new List<string> { "O tema informado já está cadastrado, informe outro tema e tempo de duração" });
                    }
                    else
                    {
                        Lectures.Add(Lecture);
                        Console.WriteLine();
                        qtd = qtd++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                IList<string> message = new List<string>(new string[] {
                    "Encontramos um problema na palestra informada:",
                    ex.Message
                });
                Utils.MensagemConsole(message);
                Console.ReadLine();
                throw ex;
            }
            return Lectures;
        }

        #endregion
    }
}