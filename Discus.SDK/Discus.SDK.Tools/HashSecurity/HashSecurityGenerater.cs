namespace Discus.SDK.Tools.HashSecurity
{
    public sealed class HashSecurityGenerater
    {
        private HashSecurityGenerater()
        {
        }
        static HashSecurityGenerater()
        {
        }

        public static ISecurity Security => new Security();

        public static IHashGenerater Hash => new HashGenerater();

        public static IAccessor Accessor => new Accessor();
    }
}
