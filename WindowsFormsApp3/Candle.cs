namespace WindowsFormsApp3
{
    internal struct Candle
    {
        public Candle(int time, float open, float high, float low, float close, int vol)
        {
            Time = time;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Vol = vol;
        }
        
        public int Time;
        public float Open;
        public float Close;
        public float High;
        public float Low;
        public int Vol;
    }
}
