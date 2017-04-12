﻿CREATE TABLE [dbo].[QRTZ_BLOB_TRIGGERS] (
    [SCHED_NAME] [nvarchar](100) NOT NULL,
    [TRIGGER_NAME] [nvarchar](150) NOT NULL,
    [TRIGGER_GROUP] [nvarchar](150) NOT NULL,
    [BLOB_DATA] [image],
    CONSTRAINT [PK_dbo.QRTZ_BLOB_TRIGGERS] PRIMARY KEY ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP])
)
CREATE TABLE [dbo].[QRTZ_CALENDARS] (
    [SCHED_NAME] [nvarchar](100) NOT NULL,
    [CALENDAR_NAME] [nvarchar](200) NOT NULL,
    [CALENDAR] [image] NOT NULL,
    CONSTRAINT [PK_dbo.QRTZ_CALENDARS] PRIMARY KEY ([SCHED_NAME], [CALENDAR_NAME])
)
CREATE TABLE [dbo].[QRTZ_CRON_TRIGGERS] (
    [SCHED_NAME] [nvarchar](100) NOT NULL,
    [TRIGGER_NAME] [nvarchar](150) NOT NULL,
    [TRIGGER_GROUP] [nvarchar](150) NOT NULL,
    [CRON_EXPRESSION] [nvarchar](120) NOT NULL,
    [TIME_ZONE_ID] [nvarchar](80),
    CONSTRAINT [PK_dbo.QRTZ_CRON_TRIGGERS] PRIMARY KEY ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP])
)
CREATE INDEX [IX_SCHED_NAME_TRIGGER_NAME_TRIGGER_GROUP] ON [dbo].[QRTZ_CRON_TRIGGERS]([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP])
CREATE TABLE [dbo].[QRTZ_TRIGGERS] (
    [SCHED_NAME] [nvarchar](100) NOT NULL,
    [TRIGGER_NAME] [nvarchar](150) NOT NULL,
    [TRIGGER_GROUP] [nvarchar](150) NOT NULL,
    [JOB_NAME] [nvarchar](150) NOT NULL,
    [JOB_GROUP] [nvarchar](150) NOT NULL,
    [DESCRIPTION] [nvarchar](250),
    [NEXT_FIRE_TIME] [bigint],
    [PREV_FIRE_TIME] [bigint],
    [PRIORITY] [int],
    [TRIGGER_STATE] [nvarchar](16) NOT NULL,
    [TRIGGER_TYPE] [nvarchar](8) NOT NULL,
    [START_TIME] [bigint] NOT NULL,
    [END_TIME] [bigint],
    [CALENDAR_NAME] [nvarchar](200),
    [MISFIRE_INSTR] [int],
    [JOB_DATA] [image],
    CONSTRAINT [PK_dbo.QRTZ_TRIGGERS] PRIMARY KEY ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP])
)
CREATE INDEX [IX_SCHED_NAME_JOB_NAME_JOB_GROUP] ON [dbo].[QRTZ_TRIGGERS]([SCHED_NAME], [JOB_NAME], [JOB_GROUP])
CREATE TABLE [dbo].[QRTZ_JOB_DETAILS] (
    [SCHED_NAME] [nvarchar](100) NOT NULL,
    [JOB_NAME] [nvarchar](150) NOT NULL,
    [JOB_GROUP] [nvarchar](150) NOT NULL,
    [DESCRIPTION] [nvarchar](250),
    [JOB_CLASS_NAME] [nvarchar](250) NOT NULL,
    [IS_DURABLE] [bit] NOT NULL,
    [IS_NONCONCURRENT] [bit] NOT NULL,
    [IS_UPDATE_DATA] [bit] NOT NULL,
    [REQUESTS_RECOVERY] [bit] NOT NULL,
    [JOB_DATA] [image],
    CONSTRAINT [PK_dbo.QRTZ_JOB_DETAILS] PRIMARY KEY ([SCHED_NAME], [JOB_NAME], [JOB_GROUP])
)
CREATE TABLE [dbo].[QRTZ_SIMPLE_TRIGGERS] (
    [SCHED_NAME] [nvarchar](100) NOT NULL,
    [TRIGGER_NAME] [nvarchar](150) NOT NULL,
    [TRIGGER_GROUP] [nvarchar](150) NOT NULL,
    [REPEAT_COUNT] [int] NOT NULL,
    [REPEAT_INTERVAL] [bigint] NOT NULL,
    [TIMES_TRIGGERED] [int] NOT NULL,
    CONSTRAINT [PK_dbo.QRTZ_SIMPLE_TRIGGERS] PRIMARY KEY ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP])
)
CREATE INDEX [IX_SCHED_NAME_TRIGGER_NAME_TRIGGER_GROUP] ON [dbo].[QRTZ_SIMPLE_TRIGGERS]([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP])
CREATE TABLE [dbo].[QRTZ_SIMPROP_TRIGGERS] (
    [SCHED_NAME] [nvarchar](100) NOT NULL,
    [TRIGGER_NAME] [nvarchar](150) NOT NULL,
    [TRIGGER_GROUP] [nvarchar](150) NOT NULL,
    [STR_PROP_1] [nvarchar](512),
    [STR_PROP_2] [nvarchar](512),
    [STR_PROP_3] [nvarchar](512),
    [INT_PROP_1] [int],
    [INT_PROP_2] [int],
    [LONG_PROP_1] [bigint],
    [LONG_PROP_2] [bigint],
    [DEC_PROP_1] [numeric](13, 4),
    [DEC_PROP_2] [numeric](13, 4),
    [BOOL_PROP_1] [bit],
    [BOOL_PROP_2] [bit],
    CONSTRAINT [PK_dbo.QRTZ_SIMPROP_TRIGGERS] PRIMARY KEY ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP])
)
CREATE INDEX [IX_SCHED_NAME_TRIGGER_NAME_TRIGGER_GROUP] ON [dbo].[QRTZ_SIMPROP_TRIGGERS]([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP])
CREATE TABLE [dbo].[QRTZ_FIRED_TRIGGERS] (
    [SCHED_NAME] [nvarchar](100) NOT NULL,
    [ENTRY_ID] [nvarchar](95) NOT NULL,
    [TRIGGER_NAME] [nvarchar](150) NOT NULL,
    [TRIGGER_GROUP] [nvarchar](150) NOT NULL,
    [INSTANCE_NAME] [nvarchar](200) NOT NULL,
    [FIRED_TIME] [bigint] NOT NULL,
    [SCHED_TIME] [bigint] NOT NULL,
    [PRIORITY] [int] NOT NULL,
    [STATE] [nvarchar](16) NOT NULL,
    [JOB_NAME] [nvarchar](150),
    [JOB_GROUP] [nvarchar](150),
    [IS_NONCONCURRENT] [bit],
    [REQUESTS_RECOVERY] [bit],
    CONSTRAINT [PK_dbo.QRTZ_FIRED_TRIGGERS] PRIMARY KEY ([SCHED_NAME], [ENTRY_ID])
)
CREATE TABLE [dbo].[QRTZ_LOCKS] (
    [SCHED_NAME] [nvarchar](100) NOT NULL,
    [LOCK_NAME] [nvarchar](40) NOT NULL,
    CONSTRAINT [PK_dbo.QRTZ_LOCKS] PRIMARY KEY ([SCHED_NAME], [LOCK_NAME])
)
CREATE TABLE [dbo].[QRTZ_PAUSED_TRIGGER_GRPS] (
    [SCHED_NAME] [nvarchar](100) NOT NULL,
    [TRIGGER_GROUP] [nvarchar](150) NOT NULL,
    CONSTRAINT [PK_dbo.QRTZ_PAUSED_TRIGGER_GRPS] PRIMARY KEY ([SCHED_NAME], [TRIGGER_GROUP])
)
CREATE TABLE [dbo].[QRTZ_SCHEDULER_STATE] (
    [SCHED_NAME] [nvarchar](100) NOT NULL,
    [INSTANCE_NAME] [nvarchar](200) NOT NULL,
    [LAST_CHECKIN_TIME] [bigint] NOT NULL,
    [CHECKIN_INTERVAL] [bigint] NOT NULL,
    CONSTRAINT [PK_dbo.QRTZ_SCHEDULER_STATE] PRIMARY KEY ([SCHED_NAME], [INSTANCE_NAME])
)
ALTER TABLE [dbo].[QRTZ_CRON_TRIGGERS] ADD CONSTRAINT [FK_dbo.QRTZ_CRON_TRIGGERS_dbo.QRTZ_TRIGGERS_SCHED_NAME_TRIGGER_NAME_TRIGGER_GROUP] FOREIGN KEY ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) REFERENCES [dbo].[QRTZ_TRIGGERS] ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP])
ALTER TABLE [dbo].[QRTZ_TRIGGERS] ADD CONSTRAINT [FK_dbo.QRTZ_TRIGGERS_dbo.QRTZ_JOB_DETAILS_SCHED_NAME_JOB_NAME_JOB_GROUP] FOREIGN KEY ([SCHED_NAME], [JOB_NAME], [JOB_GROUP]) REFERENCES [dbo].[QRTZ_JOB_DETAILS] ([SCHED_NAME], [JOB_NAME], [JOB_GROUP])
ALTER TABLE [dbo].[QRTZ_SIMPLE_TRIGGERS] ADD CONSTRAINT [FK_dbo.QRTZ_SIMPLE_TRIGGERS_dbo.QRTZ_TRIGGERS_SCHED_NAME_TRIGGER_NAME_TRIGGER_GROUP] FOREIGN KEY ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) REFERENCES [dbo].[QRTZ_TRIGGERS] ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP])
ALTER TABLE [dbo].[QRTZ_SIMPROP_TRIGGERS] ADD CONSTRAINT [FK_dbo.QRTZ_SIMPROP_TRIGGERS_dbo.QRTZ_TRIGGERS_SCHED_NAME_TRIGGER_NAME_TRIGGER_GROUP] FOREIGN KEY ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) REFERENCES [dbo].[QRTZ_TRIGGERS] ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP])
CREATE TABLE [dbo].[__MigrationHistory] (
    [MigrationId] [nvarchar](150) NOT NULL,
    [ContextKey] [nvarchar](300) NOT NULL,
    [Model] [varbinary](max) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201702151626313_InitialMigration', N'QuartzWebTemplate.Quartz.Configuration',  0x1F8B0800000000000400ED5DDB72DB38127DDFAAFD07951EB732962D27994CCA9E294562BCCAC89286A4B233F3A2A22958610D456A7871D9BBB55FB60FFB49FB0B0B883700044880175D1255F26049E041A3D1DD68349A8DFFFDE7BF373F3D6FECCE13F07CCB756EBB571797DD0E704C776539EBDB6E183C7EF7AEFBD38F7FFDCB8DB2DA3C773E27EDAE513BF8A4E3DF76BF04C1F67DAFE79B5FC0C6F02F3696E9B9BEFB185C98EEA667ACDC5EFFF2F287DED5550F40882EC4EA746ED4D009AC0DD87D801F87AE63826D101AF6BDBB02B61F7F0F7FD176A89DA9B101FED630C16DF797D0F0827FFE033CE860B3B58D005C44DF5C8C1E204C009E836E67605B06A44C03F663B763388E1B1801A4FBFDC2075AE0B9CE5ADBC22F0C5B7FD902D8EED1B07D108FE77DD65C7468977D34B45EF6600265867EE06E2401AFAE635EF5E8C72B71BC9BF2127253815C0F5ED0A8771C85CC848CFB60BB0FBA67ADD7C0EB76E83EDF0F6D0FB52FE0FB0ED402FE0505F6AA937BE4552A4150D0D0BF579D616807A1076E1D10069E61BFEACCC307DB327F062FBAFB07706E9DD0B6F131C051C0DF882FE05773CFDD022F7851C1633C3224382BF467B7D32B6F1D932CDBFECE73C32DFDC04D8F263005C8D316F1160A25D4B76EE7DE789E00671D7C819A780935ECA3F50C56C937B1942E1C0BAA277C28F042F8710AF9633CD820FD1D9B3B2D703D70071CE041DEAFE64610000F0AE1D47572A3A4492418C227F2CD311019CFC2B15289546264044642E107CB31BC1782C2FED5EBEF5FBFBB7EFBFA7B36A1780737BD4C8B4B757B68D8C059190D287682744A5A9DD0CC7AE0ABD05272805C2AFBC741650D05C851565D23E0F2DFD86A87819D925E9C57BBF36AD79AB24395509EB71EF0FD9DCBCAA7B35F91CE122EC16DC5EF90CEF1AAA0EF77625D533D4D8D276BBD6310D5273205A94D5181BD6BE37FB1B6D1168436154BA2FD47CFDDA8AE9D374F78B3A5E6869E8984D32D6FAB1BDE1A04356C6463F6F16C1BCFB6F16C1B532A3FB90F6DB1B1B4E3D65853DCF308F8A6676D83E2B5A02FD879715F53F01C7CB43C809680A4B3B113BC7D5DF6E0DC034F151FB45C0FDA4FECA1EBBEA0AC6A509E0A45E16D2BCB63D439EAB5687D6CA36B38602F60B05812467156D2F3D4F03EA9B8B37BCB7F84C23476FCC093128C4F8DC609CA1C166223D48ED3923822224E4BE2E0C80C01310C048665730780F784B526C96736627A5CEC962C7FAB8C74CD827E1028E33FD1AA6806B80D9973C06F5D6516101AFC56642C59B3B2C1B05B7247C369CE1A8E94278CC94C5D5F38853A256F38F558C4DA7EBD5E708BAE5B63041EB9F7BB4F57107263681BBE5FB6E0B7E3F48EFD51E8A1F6E952EE426B6538158020634DF83FF43CE004F5E116DB159C37C2CBA888A5823F43E007BE0A4CF709782F75F1F6EBFCC40B84DF86E3C05AA38A5D8CCACB13E545D45DA208B8535AA6CE419B73D0A62D2A55B005463074C3CCFEC6BB39698B8980E0B3C07B32EC7A9B60B403F663FE8195245DB5E2D80D6C49583BABF20D4C2D2389EF4F9AB09219DED94C9ECDE4D94CEE2842DF5C1550F8E6AADF806B1F77D4DF5747D7AD7704CD36C13AA16061FC505FEAA189EBACE9AECAA3A7C9537DA9A746C024BA829FAD0D5AF7E61EFC2B4E3785CCD54C0309A3285EBF293CB45121084C772E62CFF5CB9EABBDCCD68E96F116DA92E05AE595169DA1AC1A5B6771B4535A65E120BC17947EF0152E98E9D8B804FEF0E608D6CAC39CB2B6BD50972D087E603826682B17B0B8F748596B1FEEED54A03E0CE75C56FEC0F20027B4CD459AF71630AE17C4AC1967ACBC5A4D5CF38FFAAB144239A5D509D1CB6AFC552C4FD9E0B814BEDE238152E23837423FF577EEBC6D7DD1A4114F494CBF95D0C351EDEAE5C26A88C7A19D2632D50EAB1178A724ABA4E7F515CA6ADBAE6543C6DFF083E11760FE6139F53DC818A84AB49EA74503DF774D6B37CC7C20803A682469539C5547385D299A1DEA1C13CE1354156B0B95035276DBFD5B6EFC227DA4279B641FD8F928D9CB55AE17A83400398196610F5D942067584E90D730CB31ADAD618B124401C8AAB45CE68B54EE0B92867438F42F23B005CE0A724374728F7B9CE97028035836E7373D4C33CA15867F005524D002A751A4505347EBA4605F5E5CE4655BB4438E1671BA6A50874A496A43BE0ACF9CA45D3F697D2A9DF7D318F31E758B13752E93F5B210745EBBF033D94AEA5596237A08FD2AA6E934844D5AC18AE7FE3406BD270DE365D417C97AE93B81A4A01389FDD28A559ACBBF6FAD2A21E834A44B4AA54AE6FB3446DCB83E459B2A54E2073E01BC44A7765102ACF44F2E0CB1F0411C89F0E30D282DFE08570301A6A558151BBFDBC9B67398E0135573721A95874C5E41E2E165451244C03209E1E2E1564000B2044E062ADDB7F0C0B00DA3001CE1C2F12029775D10365BB88A7071474500183FB7E6C19227E502A028B2CD038B0E320440E880300F301F8A16E12711BFE3F2938A1A52C098EE33C593CEC7C6DA97266FD3264A26AA920E9856969CE19389A350A8B8DED051449231824C2BC8D564334E706F5D61774D0D95D6E91236966FA60527A8062379D9387C4E8AECA4AAECA518BC240C9900334BB64EED7293FB56299B95422EB3B4D34C0D915C514B3858E623D7675F12434EDD9DF4B79B5E541231FEE2A6C7A99D78736F6CB796B3C66A29C6DF74B4A890E2F03B4DBEA2E026C2E8993EA3B0604A6DDA53E07AC61A50BFC2AE21A57009F403F4CACF838122E8C3D526D78C76EE388B50D21BC77FCBCF65B22E250FA2BF7177B2A8B864CEF9CB3BCB31EC4738F40DF2EC77671A944C14227450DD4BC3363CFE51D1D0B5C38D137F3DFCBB325A4E07F70ACB77E781112E3F0EA7ABE3BB3B45AD0A18EF08588877EA6C319781CC4AE7E1701F26B30FCBD1401F30B72A3D8AFDB99D496EFE29A5A4E54A58EA3217BF05914BF707D5E48DFFF85E848D2CC080E30D0713653A1AC88B5BB66162C11D9B68E0CB4B1BD281EDF62A0A4811C2D920254247553723444F9D4D97CAAF7355D1B4F16C2A452956B48CA0737CAF2C7F9F4D95E578746412DDA634D793E4B3149741A667BE38DA27B8A8CAD2961D07D348D23411D50170B491A20DD5F15C975429B218140E38557ED5971FC7AAB244EA258349D689C231A1D27FAE8A99A42A9378E3993AD67FAB202871608525289A3ED0ABC85E542D8A85A8FF369702C46A4011AAA60F545D9A756925281C0AAEFED2402D78276411281CF27EACED04653CD574A69B52A06C795FF8D371BAC25828AB85F5811BD3135C210A9EDFCB1AF12DD85FB2020B4DDE7032D034E9E162755570C0B1B61C2DD4C187892418F55A0205399D4D87F0FF425595A92E078C5759A1501773A8AD0A57657998F9B720705C55F965A168BAB65495E1ECB3A24A2D1BA76456A8B0710BA6853C47AA665E4A30CE6E682AD458650D529EE7CA405F0E670B39CDA32B6C3030C7535D513F0F26B2BB33BCD806BD43D396310B9463DBA4D147032D290C76405A5D638A40CE2A93FACF499505D27D56977375365FE6D26E04B0FA1CAC5C310101AC6B0ED6B5D4FA99964320D6CEA95E618C5995042696D418B1E20938D86436BDAB40195654818D26455B566C8174DD861528CB0A2D30B1A4E8C28A2C1011FCD96C528132ACF4021B8D49DB01ED2F990AD282F525F248AAD9DE6288BD58DEB4AA00B991D7D5DF3871CF5336E1E4CB54A459D2F4C174A8485389BD7C8FE3A130837C30047B053F3FB3878B6E31C25AD2E1AC63DC77B7B60D6D60CB7840CB19E5BBB5603177C972D52C25FBD1BD58C8ECC576D25918FECC853AE0E4E5F30C5B98C85C9262B5492D87D9E7E6A3CA4273C8FD2595F3D9C6FE924C18ADB8BF2C01D9CB14B7B0F0E75E7926ACC340D39790D2E1CFE3A9FC9108FDFE33712A12831605535A95CA5C7A1CDD24ED3DFE26FD9CA6C7C5A969E5F70DE772D5A226A8EE9CFB64AD509E9AF6E2076073811A5C687FDA43DBDAADE449837BC3B11EE1521CD524E8F62F51D540E28AE2E3B92EB8E7FB2BE258A4FCCE6072DAC4EA2B142810EB5D9342EFBDE001A6CD1428C9809117E5AF3B4F86677E41C946745506D91A61C4508AC0DF54078F87DD303A967617215B1BA840384CBE1A54B5CB6ADB97A8C273DD830A084559017ABF063A770A9BBC5EF5DB320CB917CFC6CE0A3CDF76FFB57BF67D67FCEB327B7C8953BE24A87AD5997970D178DFB9ECFCBB6573D338C95734C9CD1BB1C669EE3740732EF5AFC828F42BF084C8032C007F77D9A03D3EEB71B98025D1A3651AFCD9A3FE1EB5BB90C5D55AE37013E6060BDAB5466713268648C329F20ADE9498803C349D1019A13F586B0BED62E4B0E844C87A584998387656E421A8CCC722117F5B597FA224C822AB2C5F7A164B88E4B150ECB6CC341DB2CE4C54764A85EECB2413222BCFF5A796B727BCEA6C6D2C88FC43094EE3E3DBE9F2ED7F7B16FB786D2C9DF4288E2E56FC1B4B814C145DDE58E48F9B6A6111B98ED591184755D5C1DA361205B97F67CFF9BC03FE6A77C064CA26730D97C1C94E166A793FB9EC4C51C2EA5C307656FAB3D27F1B4A8FE79D163834BB5BB2E4DC253C0BB525E4EBA691F10CD5CA9B183C35B5320891925A67DF4764A3D601C2335163AE871BE05966C1F55D157BE8B7D50391B4CAF440E550FAC228D52FE16A7F2DE2A5691E74EF991155008C6EAAFA76C2B1548E49C3E78A78AA692D8F0D4F33AD0554123C148DC2351F35AC1217118D30488B8ED092241615108112DDCAD73481F944CD564C1F37FDF2A0B60FA3AA00F9751970BDBB8BF6B7073ABEC0679376BCCE8D3CEDCF4161DEE241E7A0DDD58E9159596BADCAE7534AC0B57DDD0C566D35A2AACABD2FCCAAA8D52FABA95C4CBCA872849C3A481EDBC81DDC742A5E1B73FCA33AEA4B6224E552EA0A98062E98A97B79523BD251F66A9DECDA2D2DFB65251B8E748CC77DA54B0BAA8085C81BB80EE6AC0C02D7B19CCE208FF8FA95865581B898818294BDBAE5AC046517A89CCE08F774614ABEF4362DAC85D7A188DC8612BD2075DB5D3DB850C2A38DCB2FAAFEFB72F70649CC088DA126FC5B534A2F4DE1F699A4AE09F6872B20BB4BBC05BF5794102E35D2E25ECB7B94EA0CDBB931BBC37EE776B8CBA451F4C17822D627E5EF32FBA5DA70FBD6C6F7F389223766DAC7E01280372AA4001DE2489140DEFAC224806CC2ED3E8EB7CB741EDD12C3EC34FA89DB198A288AF591BF3886D95FBE19B7EFF960A1652385D6732E38DBD43D33ECD9A61AF1671B2D108B09AF84EA01AFADC96932A3C8657EB1E138389C42CAB9C0DE715F4623399CB26D7B6171BFE658C3D9A3357DBD4C13CCC95B515E21B766D8B397FB62EA3286E139306BEB8BB344E20E98FCCBECD06F0CE1D39B28240CBD64DF5A67103710D30126E131A66DC6CEA39BF8AF144549133A831FDA99157427075E603D1A262AB966A20AF9E87EF8CF861DC226CAE601ACC6CE2C0CB66100870C360FF60BCE0CE40017F5BFBBE886A4F966B62B57EB37310448A6858AB6CE9C0FA165AF52BA3F3202EE1C08E459C737D7A3B90CD00DF6EB9714297F853D0F28665FBA21480A64F83347339E4015DA163E9880B561BECCE39A047C90F28920D97E33B28CB5676CFC18237B1E7E8432BCDA3CFFF87FBE98CC1369BA0000 , N'6.1.3-40302')

CREATE TABLE [dbo].[ApplicationLock] (
    [Id] [uniqueidentifier] NOT NULL,
    [UtcTimestamp] [datetime] NOT NULL,
    [LockName] [nvarchar](256) NOT NULL,
    CONSTRAINT [PK_dbo.ApplicationLock] PRIMARY KEY ([Id])
)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201704120848434_AddApplicationLock', N'QuartzWebTemplate.Quartz.Configuration',  0x1F8B0800000000000400ED5DDB72DB38127DDFAAFD07951EB732962D273399943D538AC47895912585A4B233F3A2A24958610D456A7871D9BBB55FB60FFB49FB0B0B48BC00204100BCE892A892074B020F1A4077A3BBD16CFCEF3FFFBDF9F979ED749E801FD89E7BDBBDBAB8EC76806B7A96EDAE6EBB51F8F8DDDBEECF3FFDF52F378AB57EEE7C4EDA5DA376F04937B8ED7E09C3CDBB5E2F30BF80B5115CAC6DD3F702EF31BC30BD75CFB0BC5EFFF2F2C7DED5550F40882EC4EA746ED4C80DED35D87E801F879E6B824D1819CEBD67012788BF87BF685BD4CED45883606398E0B6FB2932FCF09FFF000F3A586F1C230417BB6F2E460F102604CF61B733706C0352A601E7B1DB315CD70B8D10D2FD6E11002DF43D77A56DE01786A3BF6C006CF768380188C7F32E6B2E3AB4CB3E1A5A2F7B308132A320F4D6928057D7F15CF5E8C72BCD78379D4B389B0A9CF5F0058D7A3BA3B7DDC166E3D8E6167DE2997F743B749FEF868E8FDA97CCFB16D406C10505F6AA937BE455CA4190D1D0BF579D61E484910F6E5D1085BEE1BCEACCA30788F20B78D1BD3F807BEB468E838F018E02FE467C01BF9AFBDE06F8E18B0A1EE3918DAD6EA7473ED7A31F4C1FC39ED90DF62EB2E1DF53D8B7F1E08094437AA58F2F4253874C1D84C67A93008DE0A8D1970560D8246BA1E7833BE0021F36B7E64618021F72CBD473B99DA299467F251D42FE86A2DBEDDC1BCF13E0AEC22FB7DDFE9BEFBB9D0FF633B0926F620A16AE0D251D3E14FA515314DEF432262B65BD4F9035DE3BDE83EEDBAB15F0EBB11E057658D6433ACBDAAD498FDF3A2659B6FD9DEF451B7916C76863F2CBD5E5E5FEF8254F2231216C22DF1C0391F12A1C2B954824A00632120ADFDBAEE1BF90CAE1EAF50FAFDF5E7FFFFA8762422BCBF6D070806B190D087682744A529DD05CF4C05721A5E400D97BCF71505943004A8D00398980966763BB1D06764A7271DEEDCEBB5D6BC20E454279DEF82008B6DE129BCE7E453A39B304EDFCDF219D991B51D0F75BB1AEA99EA6C693BDDA4E10D5275205A94E5181B36D137CB1373BEF9756154BA2FD07DF5BAB9E93574F78B3A5E645BE8998D3E3B7D50D7F05C21A3AB231FD78D68D67DD78D68D29951FBD87B6A691DB716B5353DEF30804A66F6FC2F2BDA02FD879795F53F01C7EB0FD38D4B3EB6CEC86DFBFE63D38F7C153C5076DCF87FA137BE8BA2FC8AB1AE4A75256A8182F12EA1CF55AB63FB6D1351CB01F164CB1248CE25AD2EBD4B09F54DED9BD1D3C42661ABB41E84B31C6C746E3043C83857084DA315A124344C468490C1C9921A00903A1613BCC01E03D61AD49F20B1B155A5CC52D8BEC2D1EE99A0DED20C09B7FA255D90A301B16AE01BB7595554068F05B91B164CD7883296EC91C0DA379D170A42C618C67EADAC229D42959C3A9C522D6F6EBB5825B34DD1A23F0C8ADDF7D9A827036868E1104DC43B9568CDE71308A7CD43EDDCA3DA8AD0CB702109C5813FE8F7C1FB8617DB8C5C682EB46581915B154F0670482305081E93D01FFA52EDE7E8D9F788308DA301C8AF6A87213A3F2F644591175B72802EE94B6A973D0E61CB4698B4A156C80110EBD28D3BFB13727AD3111107C16F84F8653CF09DEA6B8C4F3072C49BA6AC5B11B70498A3C2BBE03534B49E2FE49135A32C33BABC9B39A3CABC92D45E89BAB120ADF5CF51B30EDE38EFAFBEAE8BAF58EA0DA26A64E2858183FD4977A68E2B92BBA2B7EF43479AA2FF5D408984457F0B3BD46FBDEDC877FC599CE707235D340CC288AD76F0A0F392A0481A9E722F65C9FF75CED6DB676B48CB5D172826B95775A74866235B6CFE268A7B4CBC241F82F55329F4F60C34CC7C624F0C73747B0571EE694B5ED8D9AB72104A1E19AA0AD5CC0F2DE77C25AFB706F2B02F56118E7B2F207960738A16D2ED2BCB78071BD2066CD3863E5DDAAFEBB3609CA29ED4ED9AB2A5FE1F624F01ECEEB3D1228C58E73230A527BE7CEDFD4674D1AF194D8F45B093D1C95572F175643731C39692253EDB01A81774ABC4A5A5E5F21AFB66D5A36A4FC8D201C7E01E61FB65BDF828C81AA44EB5952340802CFB4B7C3CC0702A883469236C5B53AC2E94ABBD5A1CE31E13A4151B1D1ABC190B2DBEEDF72E317E9233DD924FBC0CE47C95EAE72BD40A101C808B40D67E8A10439C376C3BC84D9AE696F0C4794200A4056A4E5325FA4725F1037A4C3A17F19810D702D381BA28B7BDCE34C87432940DE9ADFF430C9E00B0CFB00AA8CA1054EA348A6A68ED649C6BEBCB8C8F3B668870C296274D5A00C71496A83BF4ACF9CA44D3F6979E2AEFB698C798FB2C5883AF3789D1782CE4B177E265B49BC7839A28790AF729A4E83D9A405AC7CED4F63D07B923056467D19AF73DF0924199D48EC97162C6E2EFFBEA58A43D0697097944871D6FB3446DCB83CED9C2A545D0A3E01FC44A6B65102ACEA542E0CB108401C8908620794667F84AB81108B2106DD4EE6C3C5DC9EABD29413231287AA86530499ABBE230099BCCAC4C2CB8A2D8880659CC6C4C3B5890024074E062AF57F586098E329004798822C48CAEC1784CD36C0325CDCE01100C6CFBF59B0E489BB00288BBBB3031101103AB0CC02CC87B445E693880332E7938A3E52C0980E29644F3AAF1B6BCF4D02A7559D4C74261D302D2C39052A138FA15071B9A1A391E4C4084E5A49CE67F1C409FAE815BC746AA8B44C73A691EF940B2E508D896465F5B06752C423ABE29315CC25A1C8042693E382B53B9BCCB7538BA752C8F49636BEA921923B2A670679B676FDE94B62D1A9D994FE76D3DB55F58CBFB8E931CA7FDEDC1B9B8DEDAEB072A0F1371D6D570B74F89D265F1473BDC3E89941416DCC94DAB4A7D0F38D15A07E855D434AE1161884E8D5A1070345E287D63AD78C3612199B50D21B6E07E61730D98C92D6E86FDC162D2B8A9A3322F396760CFB018E778DDC82ED8108C6085C840EAAD76A38864F1FE25870723C275ABBD9E7BCA5CF7A9A2CAB89E390BF88236667F5381A2B3D61C7A3D4E4E49C8EDCEA5072422FB51023E40CF9867982F602E479828BC05A03CC4DC417411BFE5D192DA7837B456641091F1287D3D5F1DD9DA256058C5DCC22C43B75B698CB4066B51871B8F793D9FBE568A00F8E8CEB325FAF05964B1DC56AFCC67E7C2FCC4656F4C0F1868389321D0DE4D92DF39C8BE08E8D35703BA30DEEC0DCFE8A0C528670564809D351E5F208D65367D3A5F2EB5C55346D3C9B4A518A55C123E81CDF2BCBDF675365391E1D1947B7C9CDF538F9CCC53CC834890047FB08375559DAB2FC021A499A26A2DC048E3652B4A13A9EEB9222455617C301A7CAAFFAF2C358559648BC6430C9C263382614FACF553193DC77126F3C53C7FA6F1518258EB015318AA60FF42ABCB72B3F5684A8FF369702C48A8A11A2A60F545D7AEAD2D2623814DCFDA5815AB04EC8AA6238E4FD58DB32CA78AAE985664A89B0E56DE18FC7690A6331CD16F607667057708728797E2F7BC4B7A07FC9923E3479C3C940D3A4878B15EA214223DA72B45007EF279260D47B2E14E474361DC2FF0B5555A6BA1C305EB687425DCCA1B42A4C916561E65FABC17155E5D342D1746DA92AC3D9674595DA364E49AD50E7072DA816F240B19A7AE1609CCDD094A9B1522D243FCF9581BE1CCE16729247976C29C01C4F7545FD3C98C87A6778F516DA43D396F11428C7E6A4D167442D090C76525E5D62CA40CE2293DACF49D90ED27C56977375365FE6F2B804B0FA0CAC5C750A01AC6B06D6B5D4FE99D6D720F6CEA95E618C59D98D422CA93162D538882390D9F4AE026558958E623429DAB2EA1DA4E936AC405956B9A3104B8A2EAC6A0711C19FCD261528C36A7914A315D27640FD4BE604B5A07D8984A26ABAB71C622F9A372D53413AF2BAFA1B23EE79CA2A9C7C3B8F544B9A3E980E15692AB16A0E381E0A33C80743B09A0EF9953D5C74AB20AC251DCE3A46BFBB3537B40197F1809AB395748E346BB29AA6944BE068544332B22F66C35F9850075CBC7CC2690B0B99CB56ADB6A87C987D3A1F55369A43FA9754F26F1BFE2599395CD1BFE480EC65895BD8F873EFD013DA61A0E94B48E9F097F154FE48847EA19E38158941CB8229AD72652E4F926E92F61E7F937E4EF324E31C45FEDDE9B9A4C55D1354C8D07BB22D94B0A8BD0421585FA00617DA9FCED0B1B73B79D2E0DE70ED47B815EF8A5C74FB97A80C2571DDFAF15C7DDE0B028B3816E1DF7F4E2EDB1E2E1A8F5C1B9A36367A5BC97EB451067103978EA3887D587CE97839185DC4C87D327CF30BCA4BA2AF136FEE565466F6A060BD9412FD55F4EE58A9F354F240E1962550620523AF644EB75556646BFE114329037F531D3C1E76C3E858D6E30ED95E43FD85C3E4ABBB55BB7CBA7D8E2A3D563F288350949589740D74E61236795DF2B7A518722F928E5D0B3CDF76FFB57DF65D67FCEB327B7C8953BE24A87AD599F970CF7ED7B9ECFCBB6575D338C95734C9CD2BB1C669EE3740732EF3B24C29F42BCC0991865902FEF6B2417D7C96633E8325C1BB651A7BDBA3FC1EB5B99085355B9BE126D40D16336D8DCE26540C9105556AE87354401E9ACE47DDA13FD82B1B39917258741E6A3DAC244A1F1B2BF21054E269198BF3FC2336F82E07B54C2BCB9792C6F25159532876FB6D9A8D5A67252A1BA542F7DF92F9A895D7FA63CBEE09ABDA621B1B22FB4C88D1F8F83C5DB6FE6F4F631FAF8EA5734EC5D1C58AF96319A889A0CB2B8BFC695F2D2C22D5B43A52C1496175B0B6954449EAE5D9723E7BC05FAD074C66CC16EEE13238D9C14E2DEB27971C2B4A589D0B03CF427F16FA6F43E8F1B4DF1283667BEB9D9CB9842701B7847CDD34329E205CD989C133832B831019C175FC3E2219B80E109E081CCF7AB406BE6D965CC757B1877E5B3D1039C38516A81C4A5F18A5FAA57AEDEF45AC2CD983FA9E195125C0E8E6B96F271C4BA5F8347CAE8867FAD6B2D8F02CDF5A409CE0A16814AEF9A86195B8886884419A7584B624B1A8800894A82B5F530556CD0A92547DCCECD783EA3E8CAA12E4D73CE07A7791EDCF073ABEC067937ABCCE0D5BEDAF4169DAE841D7A0DDDDAE20B1B5D65E954F6795806BFBFA28ACEAF18EAA2AF738155627AE7EF954E5CB01CA0A77C88983E4B18DDCC14DA7E23550C73FAAA3BEF449922FA5AE746AE0C2A8BA97A1B5C31DBC371B65F76E69DEE755CC38D2311EF7154D2D880216226FE07AA7B330085CAF743A833CE2EB941A1605E282140A52F62AA6B310F02E443A9D11EEE902A47C097C9A59C92B5672D71865DE06EBA2A3DDFB69B75DEBC1831CBE735C6ADE862472195251B79F54FDF7E5F6C59578FE35A19EB3D2CE85DD663F33FB4C32E604FBC3E5BEB84BBC05BB5794872E35D2F25EF93D4A7586398C85DD61BF333BDC26F028FA603C11EB9332B30BFBA5DA30FBD6C6F7F389223766DAB4611280372AA5009D1D4991405EFA544800D984D97D1CE697E99CA134B29F989DA140A6581FF97BA30AFBCB3763F63D1F2CB46CA45069CF05579BBA66AA78B5A946ECD546FBD262C22A9C7BC05BAB72925C50DA34BFC731EC2A46F9EC5C3CF1B8EFA2921C0E2F5A505AD2B1B9A961B8864DDF2ED5C4E4E4B528AB7C5F33D3B397EBA2EA4E4C81E55078A382F894485C01952F6100CDD5C8456FB1EF3E8D4060AF32881B88E902933054D33663F7D14BCC668AA2A409FDE200D43316B462077E683F1A262AB467A27B11DC55B7F3D97022D844593F006BECCEA270138570C860FDE0BCE09381ECEEB2FEB7F75C9134DFCCB6458A83268600C9B4D18BFF33F77D643B564AF78782383F030219F477007EBF5B4BE81F8460F592224D3D5710289EBED40F49CAA2043357339E4015DA160198809561BECCE34A146C10FE4290D37E33B28D956FAC8318237B1E7E843C6CAD9F7FFA3F3C0F81D92BC10000 , N'6.1.3-40302')

