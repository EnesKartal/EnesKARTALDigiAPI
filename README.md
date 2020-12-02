# EnesKARTALDigiAPI
Digiturk Makale API Projesi

Merhaba,
Test kullanıcısı verileri 
{
"Username":"eneskartal117@gmail.com",
"Password":"digiturk2020"
}

JSON ile çalışan body'e yazılarak Api/User/Authenticate methodu çağırılarak token alınabilir. Alınan token Header'a Key=Authorization | Value= Bearer tokenbilgileri
şeklinde eklenerek authentication sağlanır.

Veritabanı kurulumu için gereken script dosyasını bu path'te bulabilirsiniz : "EnesKARTALDigiAPI.Data\DBScript\schema.sql" 

Projede kullanıdığınız tasarım desenleri hangileridir? Bu desenleri neden kullandınız?

Repository Design Pattern, veritabanı işlemleri için tek bir sınıftan yararlanarak işlem gerçekleştiriyoruz, 
bu sayede hem kod tekrarından kaçınıyor hem de Single Responsibility (Tek Sorumluluk) prensibine de uymuş oluyoruz.

Kullandığınız teknoloji ve kütüphaneler hakkında daha önce tecrübeniz oldu mu ? Tek tek yazabilir misiniz?

Evet, daha önceden tüm teknoloji ve kütüphaneleri kullanarak geliştirmelerde bulundum.

2019 Ağustos - 2020 Ağustos yılları arasında .net Core 3.1 ile PostgreSQL ve MSSQL veritabanlarıyla çalışabilen Web API geliştirmesinde bulundum.
[Entity Framework, Authentication, Logging,Cache, Github, Dependency Injection MSSQL]

2017 Ağustos - 2019 Ağustos ayları arasında ASP.net MVC MSSQL veritabanlı web uygulamaları geliştirdim.
[Entity Framework, MSSQL, Authentication, Cache, Github, Solid Principle,Repository Design Pattern]

Daha geniş vaktiniz olsaydı projeye neler eklemek isterdiniz?

UserController içerisine parola sıfırlamak için bir method oluşturabilirdim. Bu durumda EmailHelper ekleyerek kullanıcı bilgilerini posta üzerinden doğrulayabilirdim.

CacheHelper'ı daha yaygın kullanabilirdim, Listelemelerde cache'den çağırıp insert,update veya delete işlemleri sonrasınca cache sıfırlayabilirdim.

Eklemek istediğiniz bir yorumunuz var mı?

Öncelikle çalıştığım projeyi incelediğiniz için teşekkür ederim, söz konusu olan bu case'i hem geçmiş projelerimden hem de internet üzerinden yaptığım araştırmalar ile hazırladım.

Saygılarımla...
