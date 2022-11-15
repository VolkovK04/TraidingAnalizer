using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WindowsFormsApp3
{
    public enum Period
    {
        tick = 1,
        m1,
        m5,
        m10,
        m15,
        m30,
        h1,
        d1,
        w1,
        mn1
    }
    public enum Code
    {
        GAZP = 16842,
        POLY = 175924,
        PLZL = 17123,
        SBER = 3,
        LKOH = 8
    }
    internal static class DataRequester
    {
        static int market = 1;//id мосбиржи
        public static void GetDataFile(string fileName, Code code, DateTime startDate, DateTime endDate, Period period)//скачивает необходимый файл с сайта
        {
            string url = $"http://export.finam.ru/f?market={market}&em={(int)code}&apply=0&df={startDate.Day}&mf={startDate.Month - 1}&yf={startDate.Year}&dt={endDate.Day}&mt={endDate.Month - 1}&yt={endDate.Year}&p={(int)period}&dtf=1&tmf=1&MSOR=1&mstime=on&mstimever=1&sep=3&sep2=1&datf=5&at=0";
            using (var client = new WebClient())
                client.DownloadFileAsync(new Uri(url), fileName);
        }
        public static List<Candle> ToDataList(string fileName)//формирует список из свечей
        {
            StreamReader sr = new StreamReader(fileName, Encoding.Default);
            List<Candle> data = new List<Candle>();
            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Replace('.', ',').Split(';');
                Candle candle = new Candle(Convert.ToInt32(line[1]), Convert.ToSingle(line[2]), Convert.ToSingle(line[3]), Convert.ToSingle(line[4]), Convert.ToSingle(line[5]), Convert.ToInt32(line[6]));
                data.Add(candle);
            }
            sr.Close();
            return data;
        }
        public static List<Candle> GetData(Code code, DateTime startDate, DateTime endDate, Period period)//сразу возвращает списов свечей по запросу
        {
            string fileName = code.ToString()+"_"+DateTime.Now.ToString("yyyyMMddHHmmss")+".txt";
            GetDataFile(fileName, code, startDate, endDate, period);
            return ToDataList(fileName);
        }
    }
}
