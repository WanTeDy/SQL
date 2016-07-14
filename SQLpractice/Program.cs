using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

/*
 * Показать 5 самых популярных слов, отправленных в личных сообщениях, и их общее количество 
(решение с помощью Entity Framework либо на чистом T-SQL).
 * */
namespace SQLpractice
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PracticeEntities();
            var query = from c in context.Messages
                        select c.mess;
            List<String> mess = query.ToList();
            List<String> singleWord = new List<string>();
            string[] temp;
            foreach(var el in mess)
            {
               temp = Regex.Replace(el, "[!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~\\\\\\d]+", " ").Split( new char[] {' ', '\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);  
               foreach(var str in temp)
               {
                   singleWord.Add(str);
               }
                
            }
            Dictionary<string, int> words = new Dictionary<string, int>();
            foreach(var el in singleWord)
            {
                if(!words.ContainsKey(el))
                {
                    words.Add(el, 1);
                }
                else
                {
                    words[el]++;
                }
            }
            int count = 0;
            foreach(var pair in words.OrderByDescending(pair => pair.Value))
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
                if (++count == 5)
                    break;
            }
        }
    }
}
