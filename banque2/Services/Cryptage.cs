using System.Text;

namespace banque2.Services
{
    public class Cryptage
    {
        public static string crypter(string value, IConfiguration conf)
        {
           if(string.IsNullOrEmpty(value))
            {  return ""; }
            else{
                var key = conf.GetValue<string>("CryptAndDecryptKey");
                value += key;
                var motpasseBytes= Encoding.UTF8.GetBytes(value);
                return Convert.ToBase64String(motpasseBytes);
            }
              }

        public static string decrypter(string base64data, IConfiguration conf)
        {
            if (string.IsNullOrEmpty(base64data))
            { return ""; }
            else
            { 
                var key = conf.GetValue<string>("CryptAndDecryptKey");
              
                var base64Bytes = Convert.FromBase64String(base64data);
                var result=Encoding.UTF8.GetString(base64Bytes);
                result=result.Substring(0, result.Length - key.Length);
                return result;
            }
        }
    }
}
