Create Database DigitalPersona;
CREATE TABLE FingerprintData (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Fmd VARBINARY(MAX),
	Decoded_XML varchar(MAX)
);
select * from FingerprintData;

delete from FingerprintData;

drop table FingerprintData;
	

Select Id from FingerprintData where Fmd = 0x464D52002032300001940033FEFF00000165018800C500C501000000563E806500D92F6480E200D38261805000D78B6040F300BD895D40FE006D985A4101009B8F58406E007A7757806700C0825580B000635554409900305C5480DB00869153808F01059253810D00C68B5380AA00FC3A51411200BE3651809B00D8925080830056024F809900BA8D4E80CB00FF274E4078011C924A80CA00EC2946809300EA924680CD00A8374540C000E7384480A400934F4380BF00DE364140C000F93A4140FE010F8041811F00AC9240809E00B1904080A300A5923F807E0100323F40A3008B943E411D00B7923D40B600EA383B40C000FF3F3A409900A07A39807C00E88C39809E0125A23940AB01203E3880B1012D093880980125A23741230093933680B30091503680BA0143AC3680BA00E83835409001403A3500B901133F35012300A09633011D00D491330095013B3C3200C0010F293200B10016A73000B701354D2F00A00142A52F009301303A2E008C014D992E009201479B2D00B90130512C00BF01068A2C009F014A472A009D009939290000;