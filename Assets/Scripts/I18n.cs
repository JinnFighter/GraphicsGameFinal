namespace Pixelgrid
{
    public class I18n : Mgl.I18n
    {
        protected static readonly I18n _instance = new I18n();

        public static new I18n Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
