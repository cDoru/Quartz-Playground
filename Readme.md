##What this is

This is a Quartz.net playground project which showcases the quartz.net scheduler running in a web api 2 project.
It uses autofac for dependency injection and entity code first for migrations (the quartz.net entities were reverse-engineered from the db).

##What this is not

It's more of a demo project showcasing quartz configuration with log4net logging (jobs success/errors), the crystal quartz (https://github.com/guryanovev/CrystalQuartz) dashboard for viewing the jobs/triggers and sql persistence.  Not ready for production. 

It also showcases protecting the web.config db connection string with the rsa protection cipher. 

##Usage 

Run the migrations (run the following command : Update-Database -Script -ConfigurationTypeName Configuration -Verbose -ConnectionString "Data Source=.;Initial Catalog=Quartz;Integrated Security=True;" -ConnectionProviderName "System.Data.SqlClient" - this assumes that you created a db on your local sql instance called Quartz. 

Start the project
