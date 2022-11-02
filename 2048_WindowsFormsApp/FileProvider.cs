using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace _2048_WindowsFormsApp
{
    class FileProvider
    {
        public static string path = "score.json";
        public static string pathMaxScore = "maxscore.json";
        public static List<User> userList = GetData();

        public static List<User> GetData()
        {
            string users;
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    users = Encoding.Default.GetString(buffer);
                    return JsonConvert.DeserializeObject<List<User>>(users);
                }
            }
            else
            {
                return new List<User>();
            }
        }

        public static void WriteData(User user)
        {
            userList.Add(user);
            var newUserList = JsonConvert.SerializeObject(userList);
            
            File.Delete(path);
            using (FileStream fs = new FileStream(path, FileMode.Append))
            {
                byte[] buffer = Encoding.Default.GetBytes(newUserList);
                fs.Write(buffer, 0, buffer.Length);
            }
        }

        public static int GetMaxScore()
        {
            int maxScore = 0;
            if (File.Exists(pathMaxScore))
            {
                using (FileStream fs = new FileStream(pathMaxScore, FileMode.Open))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    maxScore = int.Parse(Encoding.Default.GetString(buffer));
                }
            }
            return maxScore;
        }

        public static void WriteMaxScore(int maxScore)
        {
            File.Delete(pathMaxScore);
            using (FileStream fs = new FileStream(pathMaxScore, FileMode.Append))
            {
                byte[] buffer = Encoding.Default.GetBytes(MainForm.maxScore.ToString());
                fs.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
