1. CodeFirst yaklaşımı ile bir rest api uygulamamız olacak, bu uygulamada, kullanıcı, kategori, kitap, siparis, ve favori tabloları oluşturulacak
2. Bir MVC uygulamamız olacak, ve bu mvc uygulamamız rest api ile haberleşecek.
   MVC uygulamamızda, Anasayfa, Kitaplar, Kategori, Hakkımızda,Bize Ulaşın menüleri olacak,
   Kitaplar linkine tıklandığında kitaplar kategori,fiyat ve resimleri ile beraber görüntülenecek, kategoriler de yine         aynı şekilde olacak, kitapların ve kategorilerin detay sayfaları da olacak (ViewBag, ViewData, TempData kullanılacak)
   Hakkımızda ve Bize ulaşın sayfaları da çalışır olacak.
   Bu sayfalar responsive olacak(bootstrap kullanılacak, ürünler için bootstrap card kullanılabilir) 

3. Ürünlerde sepete ekle ve favorilere ekle butonu olacak, sepete eklenen kitaplar sağ üstte bir sepette görüntülenebilir olacak
   sepetteki ürünlerin görüntülenebilmesi için session,viewbag kullanabilirsiniz. Sepet Detay ve Siparisi Tamamla sayfaları olacak.  Siparisi tamamla sayfasında hangi kitap kim tarafından kaç adet alınmış bilgileri siparis tablosuna kaydedilecek yine favorilere eklenen kitaplar favoriler tablosuna eklenecek.  

4. MVC uygulamamızda Admin panelimiz olacak, bu Admin paneli için Identity Server da kullanabilirsiniz, ancak veritabanında da role(user,admin) tabanlı bir yapı kurgulayarak ta bu Admin panelini oluşturabilirsiniz.

5. Bu Admin panelinde kullanıcı,kategori,kitap(resimler ile beraber, resimler upload edilecek) ekleme,güncelleme silme düzenleme(crud) yapılabilecek. siparisler ve favoriler görüntelenebilir olacak. 

