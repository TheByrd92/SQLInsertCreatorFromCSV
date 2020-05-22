using System;
using System.Collections.Generic;
using System.IO;

namespace ReadCSV
{
    public class TextFileReader
    {
        private List<List<string>> Read(string path)
        {
            List<string> allContents = new List<string>();

            allContents.AddRange(File.ReadAllLines(path));

            List<List<string>> values = new List<List<string>>();

            foreach (var line in allContents)
            {
                List<string> newValues = new List<string>();
                string[] splitLine = line.Split(new char[] {','}, StringSplitOptions.None);
                foreach (var value in splitLine)
                {
                    newValues.Add(value.ToString());
                }
                values.Add(newValues);
            }

            return values;
        }

        public string CreateSQLInsert(Type type, string filePath)
        {
            string insertCmd = "INSERT INTO [dbo].[" + type.Name + "] (";
            var props = type.GetProperties();

            foreach (var col in props)
            {
                if(!col.Name.Equals("ID"))
                {
                    insertCmd += "[" + col.Name + "],";
                }
            }
            //Remove that last comma and start values
            insertCmd = insertCmd.Substring(0, insertCmd.Length - 1) + ") VALUES ";

            TextFileReader textFileReader = new TextFileReader();
            foreach (List<string> line in textFileReader.Read(filePath))
            {
                insertCmd += "(";
                for (int i = 0; i < line.Count; i++)
                {
                    if(line[i].GetType().Equals(typeof(string)) || line[i].GetType().Equals(typeof(char)))
                        insertCmd += @"'" + line[i] + @"'" + ((i < line.Count - 1) ? "," : "");
                    else
                        insertCmd += line[i] + ((i < line.Count - 1) ? "," : "");
                }
                insertCmd += "),";
            }

            insertCmd = insertCmd.Substring(0, insertCmd.Length - 1);

            return insertCmd;
        }
    }
}
