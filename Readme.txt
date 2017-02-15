
Add-Migration -ConfigurationTypeName Configuration -Verbose -ConnectionString "Data Source=.;Initial Catalog=Quartz;Integrated Security=True;" InitialMigration -ConnectionProviderName "System.Data.SqlClient"



Update-Database -Script -ConfigurationTypeName Configuration -Verbose -ConnectionString "Data Source=.;Initial Catalog=Quartz;Integrated Security=True;" -ConnectionProviderName "System.Data.SqlClient"
