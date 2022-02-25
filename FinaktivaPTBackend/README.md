Capas del web API

El proyecto cuenta con Swagger para la facil visualizacion de los modelos de las APIS

appsettings.json:
-"connectionStrings"."DefaultConnection": Es el string de coneccion a la base de datos
-"tokenManagement": Las propiedades requeridas para la encryptacion del token

Authentification
-Se encuentra el servicio necesario para crear el token

Controllers
-AuthController: Contiene la ruta de logeo que retorna el token
-UsuariosController: Donde se manejan todas las rutas de CRUD de usuario luego de recibir el token

DAO:
-AuthDao: Contine la funcion que busca el usuario en base de datos y devuelve los datos para la conversion del token en el controllador
-UsuariosDao: Contiene todas las funciones que ejecuta el UsuariosController para la creacion, busqueda, actualizacion y eliminacion en la base de datos

Models:
-Auth: contine los modelos para el logeo y creacion de token
-Usuario: contiene los modelos para la creacion, busqueda, actualizacion y eliminacion de usuarios 

PD:
-El archivo DBFinaktiva son los scripts para la base de datos el cual incluye las tablas y los SP necesarios para el correcto funcionamiento de la API 
-Para facilitar el proceso deje un usuario creado en la tabla usuarios el cual el usuario es 'Darkan1' con password '1921' 
-Las Password estan encryptadas en varbinary(50) desde la base de datos

