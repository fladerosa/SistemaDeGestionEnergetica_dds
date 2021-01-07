# dds-tp-2018-grupo-01
10/12-2018
MONGODB
Para poder probar como se genera la base y las colecciones desde la aplicacion SGE, se debe instalar localmente la version de MONGO segun la arquitectura y SO
de la pc de cada uno. Ejecutar mongod.exe (para conectar con el servidor) y luego mongo.exe (para la conexion del cliente).
Si bien se puede usar desde consola, en mi caso instale MongoBooster, cuya interfaz me permite trabajar parecido a sqlServer y poder ver las colecciones creadas y los documentos de cada una.

-----------------------------------------------------------------------------------------------
27-10-2018
Registracion: Todos los que se registren desde la pagina, asumen el rol de clientes, por defecto. 
ADMIN Se carga un super admin desde la clase startup.cs, mantiene las vinculaciones en ambos contextos.
CLIENTE (FEO)se tuvo que harcodear las fk a transformador y categoria, para poder instanciar un cliente desde la pagina de registracion..
Login: Trae los usuarios registrados de la pagina, sean clientes o administradores. No esta trayendo los usuarios que se cargaron con los casos de prueba.

----------------------------------------------------------------------------------------------
01-10-2018
Primera Aproximacion WEB -LOGIN (Aun en Desarrollo)
Ejecutar las Pruebas unitarias y probar con alguno de los username que se carguen en la base SGEDb.
Por el momento la contrasenia es 1234. 

-----------------------------------------------------------------------------------------------

10-09-2018
ATENCION
(Por el momento)
Para poder inicializar la creacion de La BD SGEDb, con las tablas que se persistieron 
y sus mapeo relacional correspondiente, se debe setear el el Data Source del ConnectionString 
(por el nombre de tu Servidor Local de SQL SERVER) del archivo App.Config que se encuentra en: SGE.Test.  


-----------------------------------------------------------------------------------------------

Se agrega documentacion de testing correspondiente a la comprobacion de los requerimientos funcionales que la aplicacion web debe cumplir.

