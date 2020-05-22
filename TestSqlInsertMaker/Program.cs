using ReadCSV;
using System;

namespace TestSqlInsertMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            TextFileReader txtFR = new TextFileReader();

            txtFR.CreateSQLInsert(typeof(TestTablecs), @"yourFileHere.txt");
        }
    }
}
