UPDATE [pendaftaran] set tahun = "2016" where id in (select id from [pendaftaran] where id between 20 and 100) 

