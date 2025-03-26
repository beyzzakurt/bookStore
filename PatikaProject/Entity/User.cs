namespace PatikaProject.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }

    }
}
//Accesss Token: OAuth 2.0 protokolüne göre RFC 7519 standardına göre belirli bir expire süresine sahip olarak üretilen güvenlik anahtarıdır.
//Token'ın kendisidir.


//Refresh Token: Access Token'ın süresi dolduğunda kullanıcının oturumunu sonlandırmadan yeni bir access token ın üretilmesini sağlar.
//İlk access token alındığında bearberinde bir refresh token'da üretilir. Access token'ın süresi dolduğunda kullanıcıyı yeniden loginden geçirmek yerine
//refresh token gönderilerek yeni bir access token alınabilir. Böylece kullanıcı kesintiye uğramadan işlemlerini yapmaya devam edebilir.