‡
C:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Core\Interfaz(puertos)\IAsistenciaRepository.cs
	namespace 	

ControlFit
 
. 
Domain 
. 
Interfaz_puertos_ -
{		 
public

 

	interface

 !
IAsistenciaRepository

 *
{ 
Task 
RegistrarAsync 
( 

Asistencia &

asistencia' 1
)1 2
;2 3
Task 
< 
bool 
> 
YaIngresoHoyAsync $
($ %
int% (
	miembroId) 2
)2 3
;3 4
Task 
< 
int 
> &
ObtenerIngresosSemanaAsync ,
(, -
int- 0
	miembroId1 :
): ;
;; <
Task 
< 
List 
< 

Asistencia 
> 
> 
ObtenerTodosAsync 0
(0 1
)1 2
;2 3
Task 
< 
List 
< 

Asistencia 
> 
> #
ObtenerTodosPorGimnasio 6
(6 7
int7 :

gimnasioId; E
)E F
;F G
Task   
<   
List   
<   

Asistencia   
>   
>   $
ObtenerPorMiembroEnRango   7
(  7 8
int  8 ;
	miembroId  < E
,  E F
DateTime  G O
fechaInicio  P [
,  [ \
DateTime  ] e
fechaFin  f n
,  n o
int  p s

gimnasioId  t ~
)  ~ 
;	   Ä
}!! 
}"" Í
~C:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Core\Interfaz(puertos)\IMembresiaRepository.cs
	namespace 	

ControlFit
 
. 
Domain 
. 
Interfaz_puertos_ -
{		 
public

 

	interface

  
IMembresiaRepository

 )
{ 
Task 
< 
	Membresia 
? 
> 
ObtenerPorIdAsync *
(* +
int+ .
id/ 1
)1 2
;2 3
Task 
< 
	Membresia 
? 
> )
ObtenerPorIdValidandoGimnasio 6
(6 7
int7 :
id; =
,= >
int? B

gimnasioIdC M
)M N
;N O
Task 
< 
	Membresia 
> 

CrearAsync "
(" #
	Membresia# ,
	membresia- 6
)6 7
;7 8
Task 
< 
List 
< 
	Membresia 
> 
> 
ListarTodos )
() *
)* +
;+ ,
Task!! 
<!! 
List!! 
<!! 
	Membresia!! 
>!! 
>!! "
ListarTodosPorGimnasio!! 4
(!!4 5
int!!5 8

gimnasioId!!9 C
)!!C D
;!!D E
Task## 
<## 
bool## 
>## 
ActualizarAsync## "
(##" #
	Membresia### ,
	membresia##- 6
)##6 7
;##7 8
Task%% 
EliminarAsync%% 
(%% 
int%% 
id%% !
)%%! "
;%%" #
}&& 
}'' ‘
|C:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Core\Interfaz(puertos)\IMiembroRepository.cs
	namespace 	

ControlFit
 
. 
Domain 
. 
Interfaz_puertos_ -
{		 
public

 

	interface

 
IMiembroRepository

 '
{ 
Task 
< 
Miembro 
? 
> 
ObtenerPorIdAsync (
(( )
int) ,
id- /
)/ 0
;0 1
Task 
< 
Miembro 
? 
> )
ObtenerPorIdValidandoGimnasio 4
(4 5
int5 8
id9 ;
,; <
int= @

gimnasioIdA K
)K L
;L M
Task 
< 
Miembro 
> 

CrearAsync  
(  !
Miembro! (
miembro) 0
)0 1
;1 2
Task 
< 
List 
< 
Miembro 
> 
> 
ListarTodos '
(' (
)( )
;) *
Task"" 
<"" 
List"" 
<"" 
Miembro"" 
>"" 
>"" "
ListarTodosPorGimnasio"" 2
(""2 3
int""3 6

gimnasioId""7 A
)""A B
;""B C
Task$$ 
<$$ 
bool$$ 
>$$ 
ActualizarAsync$$ "
($$" #
Miembro$$# *
miembro$$+ 2
)$$2 3
;$$3 4
Task&& 
EliminarAsync&& 
(&& 
int&& 
id&& !
)&&! "
;&&" #
}'' 
}(( ¿
}C:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Core\Interfaz(puertos)\IGimnasioRepository.cs
	namespace 	

ControlFit
 
. 
Domain 
. 
Interfaz_puertos_ -
{		 
public

 

	interface

 
IGimnasioRepository

 (
{ 
Task 
< 
Gimnasio 
? 
> 
ObtenerPorIdAsync )
() *
int* -
id. 0
)0 1
;1 2
Task 
< 
bool 
? 
> !
ObtenerPorNombreAsync )
() *
string* 0
nombre1 7
,7 8
int9 <
idActual= E
)E F
;F G
Task 
< 
bool 
? 
> !
ObtenerPorNombreAsync )
() *
string* 0
nombre1 7
)7 8
;8 9
Task 
< 
Gimnasio 
> 

CrearAsync !
(! "
Gimnasio" *
gimnasio+ 3
)3 4
;4 5
Task 
< 
List 
< 
Gimnasio 
? 
> 
> 
ListarTodos )
() *
)* +
;+ ,
Task 
< 
bool 
> 
ActualizarAsync "
(" #
Gimnasio# +
gimnasio, 4
)4 5
;5 6
Task 
EliminarAsync 
( 
int 
id !
)! "
;" #
} 
} ≤
àC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Core\Interfaz(puertos)\IAsignacionMembresiaRepository.cs
	namespace 	

ControlFit
 
. 
Domain 
. 
Interfaz_puertos_ -
{		 
public

 

	interface

 *
IAsignacionMembresiaRepository

 3
{ 
Task 
< 
AsignacionMembresia  
>  !

CrearAsync" ,
(, -
AsignacionMembresia- @

asignacionA K
)K L
;L M
Task 
< 
AsignacionMembresia  
?  !
>! "
ObtenerPorIdAsync# 4
(4 5
int5 8
id9 ;
); <
;< =
Task 
< 
List 
< 
AsignacionMembresia %
>% &
>& '
ObtenerTodosAsync( 9
(9 :
): ;
;; <
Task 
< 
List 
< 
AsignacionMembresia %
>% &
>& '#
ObtenerTodosPorGimnasio( ?
(? @
int@ C

gimnasioIdD N
)N O
;O P
Task 
< 
List 
< 
AsignacionMembresia %
>% &
>& '%
ObtenerPorMiembroGimnasio( A
(A B
intB E
	miembroIdF O
,O P
intQ T

gimnasioIdU _
)_ `
;` a
Task   
<   
AsignacionMembresia    
?    !
>  ! "
ObtenerActivaAsync  # 5
(  5 6
int  6 9
	miembroId  : C
)  C D
;  D E
Task"" 
<"" 
bool"" 
>"" 
EliminarAsync""  
(""  !
int""! $
id""% '
)""' (
;""( )
}## 
}$$ Â
ÇC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Core\Interfaz(puertos)\IAdministradorRepository.cs
	namespace 	

ControlFit
 
. 
Domain 
. 
Interfaz_puertos_ -
{		 
public

 

	interface

 $
IAdministradorRepository

 -
{ 
Task 
< 
Administrador 
? 
> !
ObtenerPorCorreoAsync 2
(2 3
string3 9
correo: @
)@ A
;A B
Task 
GuardarAsync 
( 
Administrador '
admin( -
)- .
;. /
} 
} ÷
gC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Core\Entidad\Miembro.cs
	namespace 	

ControlFit
 
. 
Domain 
. 
Entidad #
{ 
public		 

class		 
Miembro		 
{

 
public 
int 
Id 
{ 
get 
; 
private $
set% (
;( )
}* +
public 
string 
Nombre 
{ 
get "
;" #
private$ +
set, /
;/ 0
}1 2
public 
string 
Correo 
{ 
get "
;" #
private$ +
set, /
;/ 0
}1 2
public 
string 
Telefono 
{  
get! $
;$ %
private& -
set. 1
;1 2
}3 4
public 
DateOnly 
FechaNacimiento '
{( )
get* -
;- .
private/ 6
set7 :
;: ;
}< =
public 
bool 
Estado 
{ 
get  
;  !
private" )
set* -
;- .
}/ 0
public 
int 

GimnasioId 
{ 
get  #
;# $
private% ,
set- 0
;0 1
}2 3
public 
Gimnasio 
Gimnasio  
{! "
get# &
;& '
private( /
set0 3
;3 4
}5 6
public 
Miembro 
( 
string 
nombre $
,$ %
string& ,
correo- 3
,3 4
string5 ;
telefono< D
,D E
DateOnlyF N
fechaNacimientoO ^
,^ _
int` c

gimnasioIdd n
)n o
{ 	
Nombre 
= 
nombre 
; 
Correo 
= 
correo 
; 
Telefono 
= 
telefono 
;  
FechaNacimiento 
= 
fechaNacimiento -
;- .
Estado 
= 
true 
; 

GimnasioId 
= 

gimnasioId #
;# $
} 	
public"" 
Miembro"" 
("" 
)"" 
{## 	
}$$ 	
public(( 
void(( 

Actualizar(( 
((( 
String(( %
nombre((& ,
,((, -
String((. 4
correo((5 ;
,((; <
string((= C
telefono((D L
)((L M
{)) 	
Nombre** 
=** 
nombre** 
;** 
Correo++ 
=++ 
correo++ 
;++ 
Telefono,, 
=,, 
telefono,, 
;,,  
}-- 	
}.. 
}// ç4
iC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Core\Entidad\Membresia.cs
	namespace 	

ControlFit
 
. 
Domain 
. 
Entidad #
{		 
public

 

class

 
	Membresia

 
{ 
public 
int 
Id 
{ 
get 
; 
private $
set% (
;( )
}* +
public 
string 
Nombre 
{ 
get  #
;# $
private% ,
set- 0
;0 1
}2 3
public 
int 
	Duraci√≥n 
{ 
get !
;! "
private# *
set+ .
;. /
}0 1
public 
double 
Precio 
{ 
get "
;" #
private$ +
set, /
;/ 0
}1 2
public 
int 
?  
MaximoIngresosPorDia (
{) *
get+ .
;. /
private0 7
set8 ;
;; <
}= >
public 
int 
? #
MaximoIngresosPorSemana +
{, -
get. 1
;1 2
private3 :
set; >
;> ?
}@ A
public 
int !
MaximoIngresosTotales (
{) *
get+ .
;. /
private0 7
set8 ;
;; <
}= >
public 
bool 
Estado 
{ 
get  
;  !
private" )
set* -
;- .
}/ 0
public 
int 

GimnasioId 
{ 
get  #
;# $
private% ,
set- 0
;0 1
}2 3
public 
Gimnasio 
Gimnasio  
{! "
get# &
;& '
private( /
set0 3
;3 4
}5 6
public 
	Membresia 
( 
string 
nombre  &
,& '
int( +
	duraci√≥n, 4
,4 5
double6 <
precio= C
,C D
boolE I
estadoJ P
)P Q
{ 	
Nombre 
= 
nombre 
; 
	Duraci√≥n 
= 
	duraci√≥n 
;  
Precio 
= 
precio 
; 
Estado   
=   
estado   
;   
}!! 	
public## 
	Membresia## 
(## 
)## 
{## 
}## 
public%% 
	Membresia%% 
(%% 
string%% 
nombre%%  &
,%%& '
int%%( +
	duraci√≥n%%, 4
,%%4 5
double%%6 <
precio%%= C
)%%C D
{&& 	
Nombre'' 
='' 
nombre'' 
;'' 
	Duraci√≥n(( 
=(( 
	duraci√≥n(( 
;((  
Precio)) 
=)) 
precio)) 
;)) 
}** 	
public,, 
	Membresia,, 
(,, 
string,, 
nombre,,  &
,,,& '
int,,( +
	duraci√≥n,,, 4
,,,4 5
double,,6 <
precio,,= C
,,,C D
int,,E H 
maximoIngresosPorDia,,I ]
,,,] ^
int,,_ b#
maximoIngresosPorSemana,,c z
,,,z {
int,,| #
maximoIngresosTotales
,,Ä ï
,
,,ï ñ
bool
,,ó õ
estado
,,ú ¢
)
,,¢ £
{-- 	
Nombre.. 
=.. 
nombre.. 
;.. 
	Duraci√≥n// 
=// 
	duraci√≥n// 
;//  
Precio00 
=00 
precio00 
;00  
MaximoIngresosPorDia11  
=11! " 
maximoIngresosPorDia11# 7
;117 8#
MaximoIngresosPorSemana22 #
=22$ %#
maximoIngresosPorSemana22& =
;22= >!
MaximoIngresosTotales33 !
=33" #!
maximoIngresosTotales33$ 9
;339 :
Estado44 
=44 
estado44 
;44 
}55 	
public77 
	Membresia77 
(77 
string77 
nombre77  &
,77& '
int77( +
	duraci√≥n77, 4
,774 5
double776 <
precio77= C
,77C D
int77E H 
maximoIngresosPorDia77I ]
,77] ^
int77_ b#
maximoIngresosPorSemana77c z
,77z {
int77| #
maximoIngresosTotales
77Ä ï
,
77ï ñ
bool
77ó õ
estado
77ú ¢
,
77¢ £
int
77§ ß

gimnasioId
77® ≤
)
77≤ ≥
{88 	
Nombre99 
=99 
nombre99 
;99 
	Duraci√≥n:: 
=:: 
	duraci√≥n:: 
;::  
Precio;; 
=;; 
precio;; 
;;;  
MaximoIngresosPorDia<<  
=<<! " 
maximoIngresosPorDia<<# 7
;<<7 8#
MaximoIngresosPorSemana== #
===$ %#
maximoIngresosPorSemana==& =
;=== >!
MaximoIngresosTotales>> !
=>>" #!
maximoIngresosTotales>>$ 9
;>>9 :
Estado?? 
=?? 
estado?? 
;?? 

GimnasioId@@ 
=@@ 

gimnasioId@@ #
;@@# $
}AA 	
publicCC 
voidCC 

actualizarCC 
(CC  
stringCC  &
nombreCC' -
,CC- .
intCC/ 2
duracionCC3 ;
,CC; <
doubleCC= C
precioCCD J
,CCJ K
intCCL O 
maximoIngresosPorDiaCCP d
,CCd e
intCCf i$
maximoIngresosPorSemana	CCj Å
,
CCÅ Ç
int
CCÉ Ü#
maximoIngresosTotales
CCá ú
)
CCú ù
{DD 	
NombreEE 
=EE 
nombreEE 
;EE 
	Duraci√≥nFF 
=FF 
duracionFF 
;FF  
PrecioGG 
=GG 
precioGG 
;GG  
MaximoIngresosPorDiaHH  
=HH! " 
maximoIngresosPorDiaHH# 7
;HH7 8#
MaximoIngresosPorSemanaII #
=II$ %#
maximoIngresosPorSemanaII& =
;II= >!
MaximoIngresosTotalesJJ !
=JJ" #!
maximoIngresosTotalesJJ$ 9
;JJ9 :
}KK 	
}LL 
}MM Ñ
hC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Core\Entidad\Gimnasio.cs
	namespace 	

ControlFit
 
. 
Domain 
. 
Entidad #
{ 
public		 

class		 
Gimnasio		 
{

 
public 
int 
Id 
{ 
get 
; 
private $
set% (
;( )
}* +
public 
string 
Nombre 
{ 
get "
;" #
private$ +
set, /
;/ 0
}1 2
public 
DateOnly 
FechaCreacion %
{& '
get( +
;+ ,
private- 4
set5 8
;8 9
}: ;
public 
string 
	Direccion 
{  !
get" %
;% &
private' .
set/ 2
;2 3
}4 5
public 
bool 
Estado 
{ 
get  
;  !
private" )
set* -
;- .
}/ 0
public 
List 
< 
Miembro 
> 
Miembros %
{& '
get( +
;+ ,
private- 4
set5 8
;8 9
}: ;
public 
Gimnasio 
( 
string 
nombre %
,% &
string' -
	direccion. 7
,7 8
bool9 =
estado> D
)D E
{ 	
Nombre 
= 
nombre 
; 
FechaCreacion 
= 
DateOnly $
.$ %
FromDateTime% 1
(1 2
DateTime2 :
.: ;
Now; >
)> ?
;? @
	Direccion 
= 
	direccion !
;! "
Estado 
= 
estado 
; 
} 	
public 
Gimnasio 
( 
) 
{ 
} 
public!! 
void!! 

Actualizar!! 
(!! 
string!! %
nombre!!& ,
,!!, -
string!!. 4
	direccion!!5 >
)!!> ?
{"" 	
Nombre$$ 
=$$ 
nombre$$ 
;$$ 
	Direccion%% 
=%% 
	direccion%% !
;%%! "
}&& 	
public(( 
void(( 
EstablecerEstado(( $
((($ %
bool((% )
estado((* 0
)((0 1
{)) 	
Estado** 
=** 
estado** 
;** 
}++ 	
},, 
}-- ó
jC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Core\Entidad\Asistencia.cs
	namespace 	

ControlFit
 
. 
Domain 
. 
Entidad #
{ 
public		 

class		 

Asistencia		 
{

 
public 
int 
Id 
{ 
get 
; 
private $
set% (
;( )
}* +
public 
int 
	MiembroId 
{ 
get "
;" #
private$ +
set, /
;/ 0
}1 2
public 
int !
AsignacionMembresiaId (
{) *
get+ .
;. /
private0 7
set8 ;
;; <
}= >
public 
DateTime 
FechaHoraAcceso '
{( )
get* -
;- .
private/ 6
set7 :
;: ;
}< =
public 

Asistencia 
( 
int 
	miembroId 
, 
int !
asignacionMembresiaId %
)% &
{ 	
	MiembroId 
= 
	miembroId !
;! "!
AsignacionMembresiaId !
=" #!
asignacionMembresiaId$ 9
;9 :
FechaHoraAcceso 
= 
DateTime &
.& '
Now' *
;* +
} 	
} 
} ·
sC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Core\Entidad\AsignacionMembresia.cs
	namespace 	

ControlFit
 
. 
Domain 
. 
Entidad #
{ 
public		 

class		 
AsignacionMembresia		 $
{

 
public 
int 
Id 
{ 
get 
; 
private $
set% (
;( )
}* +
public 
int 
	MiembroId 
{ 
get "
;" #
private$ +
set, /
;/ 0
}1 2
public 
int 
MembresiaId 
{  
get! $
;$ %
private& -
set. 1
;1 2
}3 4
public 
DateTime 
FechaInicio #
{$ %
get& )
;) *
private+ 2
set3 6
;6 7
}8 9
public 
DateTime 
FechaFin  
{! "
get# &
;& '
private( /
set0 3
;3 4
}5 6
public 
DateTime 
FechaAsignacion '
=>( *
FechaInicio+ 6
;6 7
public 
DateTime 
FechaVencimiento (
=>) +
FechaFin, 4
;4 5
public 
bool 
Estado 
{ 
get  
;  !
private" )
set* -
;- .
}/ 0
public 
AsignacionMembresia "
(" #
)# $
{% &
}' (
public 
AsignacionMembresia "
(" #
int# &
	miembroId' 0
,0 1
	Membresia2 ;
	membresia< E
)E F
{ 	
	MiembroId 
= 
	miembroId !
;! "
MembresiaId   
=   
	membresia   #
.  # $
Id  $ &
;  & '
FechaInicio"" 
="" 
DateTime"" "
.""" #
Today""# (
;""( )
FechaFin$$ 
=$$ 
FechaInicio$$ "
.$$" #
AddDays$$# *
($$* +
	membresia$$+ 4
.$$4 5
	Duraci√≥n$$5 =
)$$= >
;$$> ?
Estado&& 
=&& 
true&& 
;&& 
}'' 	
public)) 
bool)) 
EstaVigente)) 
())  
)))  !
{** 	
return++ 
Estado++ 
&&++ 
DateTime++ %
.++% &
Today++& +
<=++, .
FechaFin++/ 7
;++7 8
},, 	
public-- 
void-- 

Desactivar-- 
(-- 
)--  
{.. 	
Estado// 
=// 
false// 
;// 
}00 	
}11 
}22 –
mC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Core\Entidad\Administrador.cs
	namespace 	

ControlFit
 
. 
Domain 
. 
Entidad #
{		 
public

 

class

 
Administrador

 
{ 
public 
int 
Id 
{ 
get 
; 
private $
set% (
;( )
}* +
public 
string 
NombreCompleto $
{% &
get' *
;* +
private, 3
set4 7
;7 8
}9 :
public 
string 
Correo 
{ 
get "
;" #
private$ +
set, /
;/ 0
}1 2
public 
string 

Contrasena  
{! "
get# &
;& '
private( /
set0 3
;3 4
}5 6
public   
int   
?   

GimnasioId   
{    
get  ! $
;  $ %
private  & -
set  . 1
;  1 2
}  3 4
public!! 
Gimnasio!! 
Gimnasio!!  
{!!! "
get!!# &
;!!& '
private!!( /
set!!0 3
;!!3 4
}!!5 6
public## 
Administrador## 
(## 
string## #
nombreCompleto##$ 2
,##2 3
string##4 :
correo##; A
,##A B
string##C I

contrasena##J T
,##T U
int##V Y
?##Y Z

gimnasioId##[ e
=##f g
null##h l
)##l m
{$$ 	
NombreCompleto%% 
=%% 
nombreCompleto%% +
;%%+ ,
Correo&& 
=&& 
correo&& 
;&& 

Contrasena'' 
='' 

contrasena'' #
;''# $

GimnasioId(( 
=(( 

gimnasioId(( #
;((# $
}++ 	
public-- 
Administrador-- 
(-- 
)-- 
{.. 	
}// 	
public55 
bool55 
ValidarPassword55 #
(55# $
string55$ *
passwordPlano55+ 8
)558 9
=>66 
BCrypt66 
.66 
Net66 
.66 
BCrypt66 
.66  
Verify66  &
(66& '
passwordPlano66' 4
,664 5

Contrasena666 @
)66@ A
;66A B
public88 
void88 
AsignarContrasena88 %
(88% &
string88& ,
passwordPlano88- :
)88: ;
{99 	

Contrasena:: 
=:: 
BCrypt:: 
.::  
Net::  #
.::# $
BCrypt::$ *
.::* +
HashPassword::+ 7
(::7 8
passwordPlano::8 E
)::E F
;::F G
};; 	
}<< 
}== 