—
{C:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Application\Servicios\UserContextService.cs
	namespace		 	

ControlFit		
 
.		 
Application		  
.		  !
	Servicios		! *
{

 
public 

class 
UserContextService #
:$ %
IUserContextService& 9
{ 
private 
readonly  
IHttpContextAccessor - 
_httpContextAccessor. B
;B C
public 
UserContextService !
(! " 
IHttpContextAccessor" 6
httpContextAccessor7 J
)J K
{ 	 
_httpContextAccessor  
=! "
httpContextAccessor# 6
;6 7
} 	
public 
int 
GetGimnasioId  
(  !
)! "
{ 	
var 
claim 
=  
_httpContextAccessor ,
., -
HttpContext- 8
?8 9
.9 :
User: >
?> ?
.? @
	FindFirst@ I
(I J
$strJ V
)V W
;W X
if 
( 
string 
. 
IsNullOrEmpty $
($ %
claim% *
?* +
.+ ,
Value, 1
)1 2
)2 3
return 
$num 
; 
return!! 
int!! 
.!! 
TryParse!! 
(!!  
claim!!  %
.!!% &
Value!!& +
,!!+ ,
out!!- 0
var!!1 4
value!!5 :
)!!: ;
?!!< =
value!!> C
:!!D E
$num!!F G
;!!G H
}"" 	
public'' 
string'' 
GetUserEmail'' "
(''" #
)''# $
{(( 	
return))  
_httpContextAccessor)) '
.))' (
HttpContext))( 3
?))3 4
.))4 5
User))5 9
?))9 :
.)): ;
	FindFirst)); D
())D E
$str))E L
)))L M
?))M N
.))N O
Value))O T
??))U W
string))X ^
.))^ _
Empty))_ d
;))d e
}** 	
public// 
int// 
GetAdministradorId// %
(//% &
)//& '
{00 	
var11 
claim11 
=11  
_httpContextAccessor11 ,
.11, -
HttpContext11- 8
?118 9
.119 :
User11: >
?11> ?
.11? @
	FindFirst11@ I
(11I J
$str11J O
)11O P
;11P Q
if22 
(22 
string22 
.22 
IsNullOrEmpty22 $
(22$ %
claim22% *
?22* +
.22+ ,
Value22, 1
)221 2
)222 3
return33 
$num33 
;33 
return55 
int55 
.55 
TryParse55 
(55  
claim55  %
.55% &
Value55& +
,55+ ,
out55- 0
var551 4
value555 :
)55: ;
?55< =
value55> C
:55D E
$num55F G
;55G H
}66 	
public<< 
bool<< 
EsSuperAdmin<<  
(<<  !
)<<! "
{== 	
return>> 
GetGimnasioId>>  
(>>  !
)>>! "
==>># %
$num>>& '
;>>' (
}?? 	
publicEE 
boolEE 
EsAdminGimnasioEE #
(EE# $
)EE$ %
{FF 	
returnGG 
GetGimnasioIdGG  
(GG  !
)GG! "
>GG# $
$numGG% &
;GG& '
}HH 	
}II 
}JJ ƒ
|C:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Application\Servicios\IUserContextService.cs
	namespace 	

ControlFit
 
. 
Application  
.  !
	Servicios! *
{ 
public 

	interface 
IUserContextService (
{ 
int 
GetGimnasioId 
( 
) 
; 
string 
GetUserEmail 
( 
) 
; 
int"" 
GetAdministradorId"" 
("" 
)""  
;""  !
bool(( 
EsSuperAdmin(( 
((( 
)(( 
;(( 
bool.. 
EsAdminGimnasio.. 
(.. 
).. 
;.. 
}// 
}00 ö
vC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Application\Servicios\ITokenService.cs
	namespace 	

ControlFit
 
. 
Application  
.  !

Repository! +
{		 
public

 

	interface

 
ITokenService

 "
{ 
string 
GenerarToken 
( 
int 
administradorId  /
,/ 0
string1 7
correo8 >
,> ?
int@ C
?C D

gimnasioIdE O
=P Q
nullR V
)V W
;W X
} 
} Ø
mC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Application\DTO\MiembroDTO.cs
	namespace 	

ControlFit
 
. 
Application  
.  !
DTO! $
{ 
public		 

class		 

MiembroDTO		 
{

 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Correo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Telefono 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
DateOnly 
FechaNacimiento '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
int 

GimnasioId 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
? 
GimnasioNombre %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
} 
} ˘0
oC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Application\DTO\MembresiaDTO.cs
	namespace 	

ControlFit
 
. 
Application  
.  !
DTO! $
{		 
public 

class 
MembresiaDTO 
{ 
public 
MembresiaDTO 
( 
int 
id  "
," #
string$ *
nombre+ 1
,1 2
int3 6
	duraci√≥n7 ?
,? @
doubleA G
precioH N
,N O
boolP T
estadoU [
,[ \
int] ` 
maximoIngresosPorDiaa u
,u v
intw z$
maximoIngresosPorSemana	{ í
,
í ì
int
î ó#
maximoIngresosTotales
ò ≠
,
≠ Æ
int
Ø ≤

gimnasioId
≥ Ω
)
Ω æ
{ 	
Id 
= 
id 
; 
Nombre 
= 
nombre 
; 
	Duraci√≥n 
= 
	duraci√≥n 
;  
Precio 
= 
precio 
; 
Estado 
= 
estado 
;  
MaximoIngresosPorDia  
=! " 
maximoIngresosPorDia# 7
;7 8#
MaximoIngresosPorSemana #
=$ %#
maximoIngresosPorSemana& =
;= >!
MaximoIngresosTotales !
=" #!
maximoIngresosTotales$ 9
;9 :

GimnasioId 
= 

gimnasioId #
;# $
} 	
public 
int 
Id 
{ 
get 
; 
private $
set% (
;( )
}* +
public 
string 
Nombre 
{ 
get "
;" #
private$ +
set, /
;/ 0
}1 2
public 
int 
	Duraci√≥n 
{ 
get !
;! "
private# *
set+ .
;. /
}0 1
public 
double 
Precio 
{ 
get "
;" #
private$ +
set, /
;/ 0
}1 2
public   
bool   
Estado   
{   
get    
;    !
private  " )
set  * -
;  - .
}  / 0
public!! 
int!!  
MaximoIngresosPorDia!! '
{!!( )
get!!* -
;!!- .
private!!/ 6
set!!7 :
;!!: ;
}!!< =
public"" 
int"" #
MaximoIngresosPorSemana"" *
{""+ ,
get""- 0
;""0 1
private""2 9
set"": =
;""= >
}""? @
public## 
int## !
MaximoIngresosTotales## (
{##) *
get##+ .
;##. /
private##0 7
set##8 ;
;##; <
}##= >
public$$ 
int$$ 

GimnasioId$$ 
{$$ 
get$$  #
;$$# $
private$$% ,
set$$- 0
;$$0 1
}$$2 3
}%% 
public** 

class** 
CrearMembresiaDTO** "
{++ 
public,, 
string,, 
Nombre,, 
{,, 
get,, "
;,," #
set,,$ '
;,,' (
},,) *
[-- 	
JsonPropertyName--	 
(-- 
$str-- $
)--$ %
]--% &
public.. 
int.. 
	Duraci√≥n.. 
{.. 
get.. !
;..! "
set..# &
;..& '
}..( )
public// 
double// 
Precio// 
{// 
get// "
;//" #
set//$ '
;//' (
}//) *
public00 
int00  
MaximoIngresosPorDia00 '
{00( )
get00* -
;00- .
set00/ 2
;002 3
}004 5
public11 
int11 #
MaximoIngresosPorSemana11 *
{11+ ,
get11- 0
;110 1
set112 5
;115 6
}117 8
public22 
int22 !
MaximoIngresosTotales22 (
{22) *
get22+ .
;22. /
set220 3
;223 4
}225 6
public33 
int33 

GimnasioId33 
{33 
get33  #
;33# $
set33% (
;33( )
}33* +
}44 
public99 

class99 "
ActualizarMembresiaDTO99 '
{:: 
public;; 
int;; 
Id;; 
{;; 
get;; 
;;; 
set;;  
;;;  !
};;" #
public<< 
string<< 
Nombre<< 
{<< 
get<< "
;<<" #
set<<$ '
;<<' (
}<<) *
[== 	
JsonPropertyName==	 
(== 
$str== $
)==$ %
]==% &
public>> 
int>> 
	Duraci√≥n>> 
{>> 
get>> !
;>>! "
set>># &
;>>& '
}>>( )
public?? 
double?? 
Precio?? 
{?? 
get?? "
;??" #
set??$ '
;??' (
}??) *
public@@ 
bool@@ 
Estado@@ 
{@@ 
get@@  
;@@  !
set@@" %
;@@% &
}@@' (
publicAA 
intAA  
MaximoIngresosPorDiaAA '
{AA( )
getAA* -
;AA- .
setAA/ 2
;AA2 3
}AA4 5
publicBB 
intBB #
MaximoIngresosPorSemanaBB *
{BB+ ,
getBB- 0
;BB0 1
setBB2 5
;BB5 6
}BB7 8
publicCC 
intCC !
MaximoIngresosTotalesCC (
{CC) *
getCC+ .
;CC. /
setCC0 3
;CC3 4
}CC5 6
publicDD 
intDD 

GimnasioIdDD 
{DD 
getDD  #
;DD# $
setDD% (
;DD( )
}DD* +
}EE 
}FF ∆
nC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Application\DTO\GimnasioDTO.cs
	namespace		 	

ControlFit		
 
.		 
Application		  
.		  !
DTO		! $
{

 
public 

class 
GimnasioCrearDTO !
{ 
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	

JsonIgnore	 
] 
public 
DateOnly 
FechaCreacion %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
	Direccion 
{  !
get" %
;% &
set' *
;* +
}, -
public 
bool 
Estado 
{ 
get  
;  !
set" %
;% &
}' (
public 
GimnasioCrearDTO 
(  
string  &
nombre' -
,- .
string/ 5
	direccion6 ?
)? @
{ 	
Nombre 
= 
nombre 
; 
FechaCreacion 
= 
DateOnly $
.$ %
FromDateTime% 1
(1 2
DateTime2 :
.: ;
Now; >
)> ?
;? @
	Direccion 
= 
	direccion !
;! "
Estado 
= 
true 
; 
} 	
} 
public 

class !
GimnasioActualizarDTO &
{ 
public   
int   
Id   
{   
get   
;   
set    
;    !
}  " #
public!! 
string!! 
Nombre!! 
{!! 
get!! "
;!!" #
set!!$ '
;!!' (
}!!) *
public## 
string## 
	Direccion## 
{##  !
get##" %
;##% &
set##' *
;##* +
}##, -
public%% 
bool%% 
Estado%% 
{%% 
get%%  
;%%  !
set%%" %
;%%% &
}%%' (
}(( 
})) √
pC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Application\DTO\AsistenciaDTO.cs
	namespace 	

ControlFit
 
. 
Application  
.  !
DTO! $
{ 
public 

class 
RegistroIngresoDTO #
{ 
public 
int 
	MiembroId 
{ 
get "
;" #
set$ '
;' (
}) *
} 
public 

class 
AsistenciaDTO 
{ 
public 
AsistenciaDTO 
( 
int  
id! #
,# $
int% (
	miembroId) 2
,2 3
int4 7!
asignacionMembresiaId8 M
,M N
DateTimeO W
fechaHoraAccesoX g
,g h
stringi o
nombreMiembrop }
,} ~
string	 Ö
nombreMembresia
Ü ï
)
ï ñ
{ 	
Id 
= 
id 
; 
	MiembroId 
= 
	miembroId !
;! "!
AsignacionMembresiaId !
=" #!
asignacionMembresiaId$ 9
;9 :
FechaHoraAcceso 
= 
fechaHoraAcceso -
;- .
NombreMiembro 
= 
nombreMiembro )
;) *
NombreMembresia 
= 
nombreMembresia -
;- .
} 	
public   
int   
Id   
{   
get   
;   
private   $
set  % (
;  ( )
}  * +
public!! 
int!! 
	MiembroId!! 
{!! 
get!! "
;!!" #
private!!$ +
set!!, /
;!!/ 0
}!!1 2
public"" 
int"" !
AsignacionMembresiaId"" (
{"") *
get""+ .
;"". /
private""0 7
set""8 ;
;""; <
}""= >
public## 
DateTime## 
FechaHoraAcceso## '
{##( )
get##* -
;##- .
private##/ 6
set##7 :
;##: ;
}##< =
public$$ 
string$$ 
NombreMiembro$$ #
{$$$ %
get$$& )
;$$) *
private$$+ 2
set$$3 6
;$$6 7
}$$8 9
public%% 
string%% 
NombreMembresia%% %
{%%& '
get%%( +
;%%+ ,
private%%- 4
set%%5 8
;%%8 9
}%%: ;
}&& 
public++ 

class++ 
FiltroAsistenciaDTO++ $
{,, 
public-- 
int-- 
?-- 
	MiembroId-- 
{-- 
get--  #
;--# $
set--% (
;--( )
}--* +
public.. 
DateTime.. 
?.. 
FechaInicio.. $
{..% &
get..' *
;..* +
set.., /
;../ 0
}..1 2
public// 
DateTime// 
?// 
FechaFin// !
{//" #
get//$ '
;//' (
set//) ,
;//, -
}//. /
}00 
}11 ≥
~C:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Application\DTO\AsignacionMembresiaCrearDTO.cs
	namespace 	

ControlFit
 
. 
Application  
.  !
DTO! $
{ 
public		 

class		 '
AsignacionMembresiaCrearDTO		 ,
{

 
public 
int 
	MiembroId 
{ 
get "
;" #
set$ '
;' (
}) *
public 
int 
MembresiaId 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} ë
sC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Application\DTO\AdministradorDTO.cs
	namespace 	

ControlFit
 
. 
Application  
.  !
DTO! $
{ 
public 

class $
RegistroAdministradorDTO )
{ 
public 
string 
nombreCompleto $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
correo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 

contrasena  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
int 
? 

GimnasioId 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
public 

class !
LoginAdministradorDTO &
{ 
public 
string 
correo 
{ 
get "
;" #
set$ '
;' (
}) *
public   
string   

contrasena    
{  ! "
get  # &
;  & '
set  ( +
;  + ,
}  - .
}!! 
public&& 

class&& 
AdministradorDTO&& !
{'' 
public(( 
int(( 
Id(( 
{(( 
get(( 
;(( 
set((  
;((  !
}((" #
public)) 
string)) 
NombreCompleto)) $
{))% &
get))' *
;))* +
set)), /
;))/ 0
}))1 2
public** 
string** 
Correo** 
{** 
get** "
;**" #
set**$ '
;**' (
}**) *
public++ 
int++ 
?++ 

GimnasioId++ 
{++  
get++! $
;++$ %
set++& )
;++) *
}+++ ,
public,, 
string,, 
GimnasioNombre,, $
{,,% &
get,,' *
;,,* +
set,,, /
;,,/ 0
},,1 2
}-- 
}.. °*
ÄC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Application\CasosUso\Ingreso\RegistrarIngreso.cs
	namespace

 	

ControlFit


 
.

 
Application

  
.

  !
CasosUso

! )
.

) *
Ingreso

* 1
{ 
public 

class 
RegistrarIngreso !
{ 
private 
readonly !
IAsistenciaRepository .!
_asistenciaRepository/ D
;D E
private 
readonly *
IAsignacionMembresiaRepository 7!
_asignacionRepository8 M
;M N
private 
readonly  
IMembresiaRepository - 
_membresiaRepository. B
;B C
private 
readonly 
IMiembroRepository +
_miembroRepository, >
;> ?
private 
readonly 
IUserContextService ,
_userContext- 9
;9 :
public 
RegistrarIngreso 
(  !
IAsistenciaRepository ! 
asistenciaRepository" 6
,6 7*
IAsignacionMembresiaRepository * 
asignacionRepository+ ?
,? @ 
IMembresiaRepository  
membresiaRepository! 4
,4 5
IMiembroRepository 
miembroRepository 0
,0 1
IUserContextService   
userContext    +
)  + ,
{!! 	!
_asistenciaRepository"" !
=""" # 
asistenciaRepository""$ 8
;""8 9!
_asignacionRepository## !
=##" # 
asignacionRepository##$ 8
;##8 9 
_membresiaRepository$$  
=$$! "
membresiaRepository$$# 6
;$$6 7
_miembroRepository%% 
=%%  
miembroRepository%%! 2
;%%2 3
_userContext&& 
=&& 
userContext&& &
;&&& '
}'' 	
public,, 
async,, 
Task,, 
EjecutarAsync,, '
(,,' (
int,,( +
	miembroId,,, 5
),,5 6
{-- 	
var.. 

gimnasioId.. 
=.. 
_userContext.. )
...) *
GetGimnasioId..* 7
(..7 8
)..8 9
;..9 :
if11 
(11 
_userContext11 
.11 
EsAdminGimnasio11 ,
(11, -
)11- .
)11. /
{22 
var33 
miembro33 
=33 
await33 #
_miembroRepository33$ 6
.336 7)
ObtenerPorIdValidandoGimnasio337 T
(33T U
	miembroId33U ^
,33^ _

gimnasioId33` j
)33j k
;33k l
if44 
(44 
miembro44 
==44 
null44 #
)44# $
throw55 
new55 
	Exception55 '
(55' (
$str55( b
)55b c
;55c d
}66 
var88 

asignacion88 
=88 
await88 "!
_asignacionRepository88# 8
.888 9
ObtenerActivaAsync889 K
(88K L
	miembroId88L U
)88U V
;88V W
if:: 
(:: 

asignacion:: 
==:: 
null:: "
||::# %
!::& '

asignacion::' 1
.::1 2
EstaVigente::2 =
(::= >
)::> ?
)::? @
throw;; 
new;; 
	Exception;; #
(;;# $
$str;;$ 7
);;7 8
;;;8 9
var== 
	yaIngreso== 
=== 
await== !!
_asistenciaRepository==" 7
.==7 8
YaIngresoHoyAsync==8 I
(==I J
	miembroId==J S
)==S T
;==T U
if?? 
(?? 
	yaIngreso?? 
)?? 
throw@@ 
new@@ 
	Exception@@ #
(@@# $
$str@@$ ?
)@@? @
;@@@ A
varBB 
	membresiaBB 
=BB 
awaitBB ! 
_membresiaRepositoryBB" 6
.BB6 7
ObtenerPorIdAsyncBB7 H
(BBH I

asignacionBBI S
.BBS T
MembresiaIdBBT _
)BB_ `
;BB` a
varDD 
ingresosSemanaDD 
=DD  
awaitDD! &!
_asistenciaRepositoryDD' <
.DD< =&
ObtenerIngresosSemanaAsyncDD= W
(DDW X
	miembroIdDDX a
)DDa b
;DDb c
ifFF 
(FF 
	membresiaFF 
.FF #
MaximoIngresosPorSemanaFF 1
.FF1 2
HasValueFF2 :
&&FF; =
ingresosSemanaGG 
>=GG !
	membresiaGG" +
.GG+ ,#
MaximoIngresosPorSemanaGG, C
.GGC D
ValueGGD I
)GGI J
{HH 
throwII 
newII 
	ExceptionII #
(II# $
$strII$ >
)II> ?
;II? @
}JJ 
varLL 

asistenciaLL 
=LL 
newLL  

AsistenciaLL! +
(LL+ ,
	miembroIdLL, 5
,LL5 6

asignacionLL7 A
.LLA B
IdLLB D
)LLD E
;LLE F
awaitNN !
_asistenciaRepositoryNN '
.NN' (
RegistrarAsyncNN( 6
(NN6 7

asistenciaNN7 A
)NNA B
;NNB C
}OO 	
}PP 
}QQ Î?
ÇC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Application\CasosUso\CRUDMiembro\MiembroService.cs
	namespace 	

ControlFit
 
. 
Application  
.  !
CasosUso! )
.) *
CRUDMiembro* 5
{ 
public 

class 
MiembroService 
{ 
private 
readonly 
IMiembroRepository +
_repo, 1
;1 2
private 
readonly 
IGimnasioRepository ,
_gimnasioRepository- @
;@ A
private 
readonly 
IUserContextService ,
_userContext- 9
;9 :
public 
MiembroService 
( 
IMiembroRepository 0
repo1 5
,5 6
ITokenService7 D
tokenServiceE Q
,Q R
IUserContextServiceS f
userContextg r
)r s
{ 	
_repo 
= 
repo 
; 
_userContext 
= 
userContext &
;& '
} 	
public   
async   
Task   
<   
Miembro   !
>  ! "
Crearmiembro  # /
(  / 0

MiembroDTO  0 :

miembroDTO  ; E
)  E F
{!! 	
if## 
(## 
_userContext## 
.## 
EsSuperAdmin## )
(##) *
)##* +
)##+ ,
throw$$ 
new$$ 
	Exception$$ #
($$# $
$str$$$ j
)$$j k
;$$k l
var&& 

gimnasioId&& 
=&& 
_userContext&& )
.&&) *
GetGimnasioId&&* 7
(&&7 8
)&&8 9
;&&9 :
if)) 
()) 

miembroDTO)) 
.)) 

GimnasioId)) %
!=))& (

gimnasioId))) 3
)))3 4
throw** 
new** 
	Exception** #
(**# $
$str**$ \
)**\ ]
;**] ^
var,, 
miembro,, 
=,, 
new,, 
Miembro,, %
(,,% &

miembroDTO,,& 0
.,,0 1
Nombre,,1 7
,,,7 8

miembroDTO,,9 C
.,,C D
Correo,,D J
,,,J K

miembroDTO,,L V
.,,V W
Telefono,,W _
,,,_ `

miembroDTO,,a k
.,,k l
FechaNacimiento,,l {
,,,{ |

miembroDTO	,,} á
.
,,á à

GimnasioId
,,à í
)
,,í ì
;
,,ì î
return-- 
await-- 
_repo-- 
.-- 

CrearAsync-- )
(--) *
miembro--* 1
)--1 2
;--2 3
}.. 	
public55 
async55 
Task55 
<55 
List55 
<55 
Miembro55 &
>55& '
>55' (
ListarMiembro55) 6
(556 7
)557 8
{66 	
var77 

gimnasioId77 
=77 
_userContext77 )
.77) *
GetGimnasioId77* 7
(777 8
)778 9
;779 :
if99 
(99 
_userContext99 
.99 
EsSuperAdmin99 )
(99) *
)99* +
)99+ ,
return:: 
await:: 
_repo:: "
.::" #
ListarTodos::# .
(::. /
)::/ 0
;::0 1
else;; 
return<< 
await<< 
_repo<< "
.<<" #"
ListarTodosPorGimnasio<<# 9
(<<9 :

gimnasioId<<: D
)<<D E
;<<E F
}== 	
publicBB 
asyncBB 
TaskBB 
<BB 
MiembroBB !
>BB! "
ObtenerMiembroIdBB# 3
(BB3 4
intBB4 7
idBB8 :
)BB: ;
{CC 	
varDD 

gimnasioIdDD 
=DD 
_userContextDD )
.DD) *
GetGimnasioIdDD* 7
(DD7 8
)DD8 9
;DD9 :
MiembroFF 
miembroFF 
;FF 
ifGG 
(GG 
_userContextGG 
.GG 
EsSuperAdminGG )
(GG) *
)GG* +
)GG+ ,
miembroHH 
=HH 
awaitHH 
_repoHH  %
.HH% &
ObtenerPorIdAsyncHH& 7
(HH7 8
idHH8 :
)HH: ;
;HH; <
elseII 
miembroJJ 
=JJ 
awaitJJ 
_repoJJ  %
.JJ% &)
ObtenerPorIdValidandoGimnasioJJ& C
(JJC D
idJJD F
,JJF G

gimnasioIdJJH R
)JJR S
;JJS T
ifLL 
(LL 
miembroLL 
==LL 
nullLL 
)LL  
throwMM 
newMM 
	ExceptionMM #
(MM# $
$strMM$ \
)MM\ ]
;MM] ^
returnOO 
miembroOO 
;OO 
}PP 	
publicUU 
asyncUU 
TaskUU 
<UU 
boolUU 
>UU 
ActualizarMiembroUU  1
(UU1 2
intUU2 5
idUU6 8
,UU8 9
stringUU: @
nombreUUA G
,UUG H
stringUUI O
correoUUP V
,UUV W
stringUUX ^
telefonoUU_ g
)UUg h
{VV 	
varWW 

gimnasioIdWW 
=WW 
_userContextWW )
.WW) *
GetGimnasioIdWW* 7
(WW7 8
)WW8 9
;WW9 :
MiembroYY 
miembroYY 
;YY 
ifZZ 
(ZZ 
_userContextZZ 
.ZZ 
EsSuperAdminZZ )
(ZZ) *
)ZZ* +
)ZZ+ ,
miembro[[ 
=[[ 
await[[ 
_repo[[  %
.[[% &
ObtenerPorIdAsync[[& 7
([[7 8
id[[8 :
)[[: ;
;[[; <
else\\ 
miembro]] 
=]] 
await]] 
_repo]]  %
.]]% &)
ObtenerPorIdValidandoGimnasio]]& C
(]]C D
id]]D F
,]]F G

gimnasioId]]H R
)]]R S
;]]S T
if__ 
(__ 
miembro__ 
==__ 
null__ 
)__  
throw`` 
new`` 
	Exception`` #
(``# $
$str``$ _
)``_ `
;``` a
miembrobb 
.bb 

Actualizarbb 
(bb 
nombrebb %
,bb% &
correobb' -
,bb- .
telefonobb/ 7
)bb7 8
;bb8 9
returncc 
awaitcc 
_repocc 
.cc 
ActualizarAsynccc .
(cc. /
miembrocc/ 6
)cc6 7
;cc7 8
}dd 	
publicii 
asyncii 
Taskii 
EliminarMiembroii )
(ii) *
intii* -
idii. 0
)ii0 1
{jj 	
varkk 

gimnasioIdkk 
=kk 
_userContextkk )
.kk) *
GetGimnasioIdkk* 7
(kk7 8
)kk8 9
;kk9 :
Miembromm 
miembromm 
;mm 
ifnn 
(nn 
_userContextnn 
.nn 
EsSuperAdminnn )
(nn) *
)nn* +
)nn+ ,
miembrooo 
=oo 
awaitoo 
_repooo  %
.oo% &
ObtenerPorIdAsyncoo& 7
(oo7 8
idoo8 :
)oo: ;
;oo; <
elsepp 
miembroqq 
=qq 
awaitqq 
_repoqq  %
.qq% &)
ObtenerPorIdValidandoGimnasioqq& C
(qqC D
idqqD F
,qqF G

gimnasioIdqqH R
)qqR S
;qqS T
ifss 
(ss 
miembross 
==ss 
nullss 
)ss  
throwtt 
newtt 
	Exceptiontt #
(tt# $
$strtt$ ]
)tt] ^
;tt^ _
awaitvv 
_repovv 
.vv 
EliminarAsyncvv %
(vv% &
idvv& (
)vv( )
;vv) *
}ww 	
}xx 
}yy ∞Y
ÜC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Application\CasosUso\CRUDMembresia\MembresiaService.cs
	namespace 	

ControlFit
 
. 
Application  
.  !
CasosUso! )
.) *
CRUDMembresia* 7
{ 
public 

class 
MembresiaService !
{ 
private 
readonly  
IMembresiaRepository - 
_membresiaRepository. B
;B C
private 
readonly 
IUserContextService ,
_userContext- 9
;9 :
public 
MembresiaService 
(   
IMembresiaRepository  4
membresiaRepository5 H
,H I
IUserContextServiceJ ]
userContext^ i
)i j
{ 	 
_membresiaRepository  
=! "
membresiaRepository# 6
;6 7
_userContext 
= 
userContext &
;& '
} 	
public 
async 
Task 
< 
	Membresia #
># $
ObtenerPorId% 1
(1 2
int2 5
id6 8
)8 9
{ 	
var 

gimnasioId 
= 
_userContext )
.) *
GetGimnasioId* 7
(7 8
)8 9
;9 :
	Membresia 
	membresia 
;  
if   
(   
_userContext   
.   
EsSuperAdmin   )
(  ) *
)  * +
)  + ,
	membresia!! 
=!! 
await!! ! 
_membresiaRepository!!" 6
.!!6 7
ObtenerPorIdAsync!!7 H
(!!H I
id!!I K
)!!K L
;!!L M
else"" 
	membresia## 
=## 
await## ! 
_membresiaRepository##" 6
.##6 7)
ObtenerPorIdValidandoGimnasio##7 T
(##T U
id##U W
,##W X

gimnasioId##Y c
)##c d
;##d e
if%% 
(%% 
	membresia%% 
==%% 
null%% !
)%%! "
throw&& 
new&& 
	Exception&& #
(&&# $
$str&&$ ^
)&&^ _
;&&_ `
return(( 
	membresia(( 
;(( 
})) 	
public00 
async00 
Task00 
<00 
	Membresia00 #
>00# $
Crear00% *
(00* +
CrearMembresiaDTO00+ <
dto00= @
)00@ A
{11 	
if22 
(22 
dto22 
==22 
null22 
)22 
throw33 
new33 
	Exception33 #
(33# $
$str33$ P
)33P Q
;33Q R
if55 
(55 
string55 
.55 
IsNullOrWhiteSpace55 )
(55) *
dto55* -
.55- .
Nombre55. 4
)554 5
)555 6
throw66 
new66 
	Exception66 #
(66# $
$str66$ N
)66N O
;66O P
if88 
(88 
dto88 
.88 
	Duraci√≥n88 
<=88 
$num88  !
)88! "
throw99 
new99 
	Exception99 #
(99# $
$str99$ D
)99D E
;99E F
if;; 
(;; 
dto;; 
.;; 
Precio;; 
<;; 
$num;; 
);; 
throw<< 
new<< 
	Exception<< #
(<<# $
$str<<$ E
)<<E F
;<<F G
if?? 
(?? 
_userContext?? 
.?? 
EsSuperAdmin?? )
(??) *
)??* +
)??+ ,
throw@@ 
new@@ 
	Exception@@ #
(@@# $
$str@@$ l
)@@l m
;@@m n
varBB 

gimnasioIdBB 
=BB 
_userContextBB )
.BB) *
GetGimnasioIdBB* 7
(BB7 8
)BB8 9
;BB9 :
ifEE 
(EE 
dtoEE 
.EE 

GimnasioIdEE 
!=EE !

gimnasioIdEE" ,
)EE, -
throwFF 
newFF 
	ExceptionFF #
(FF# $
$strFF$ ^
)FF^ _
;FF_ `
varHH 
	membresiaHH 
=HH 
newHH 
	MembresiaHH  )
(HH) *
dtoII 
.II 
NombreII 
,II 
dtoJJ 
.JJ 
	Duraci√≥nJJ 
,JJ 
dtoKK 
.KK 
PrecioKK 
,KK 
dtoLL 
.LL  
MaximoIngresosPorDiaLL (
,LL( )
dtoMM 
.MM #
MaximoIngresosPorSemanaMM +
,MM+ ,
dtoNN 
.NN !
MaximoIngresosTotalesNN )
,NN) *
trueOO 
,OO 
dtoPP 
.PP 

GimnasioIdPP 
)QQ 
;QQ 
returnSS 
awaitSS  
_membresiaRepositorySS -
.SS- .

CrearAsyncSS. 8
(SS8 9
	membresiaSS9 B
)SSB C
;SSC D
}TT 	
public[[ 
async[[ 
Task[[ 
<[[ 
List[[ 
<[[ 
	Membresia[[ (
>[[( )
>[[) *
ListarTodos[[+ 6
([[6 7
)[[7 8
{\\ 	
var]] 

gimnasioId]] 
=]] 
_userContext]] )
.]]) *
GetGimnasioId]]* 7
(]]7 8
)]]8 9
;]]9 :
if__ 
(__ 
_userContext__ 
.__ 
EsSuperAdmin__ )
(__) *
)__* +
)__+ ,
return`` 
await``  
_membresiaRepository`` 1
.``1 2
ListarTodos``2 =
(``= >
)``> ?
;``? @
elseaa 
returnbb 
awaitbb  
_membresiaRepositorybb 1
.bb1 2"
ListarTodosPorGimnasiobb2 H
(bbH I

gimnasioIdbbI S
)bbS T
;bbT U
}cc 	
publichh 
asynchh 
Taskhh 
<hh 
boolhh 
>hh 

Actualizarhh  *
(hh* +"
ActualizarMembresiaDTOhh+ A
dtohhB E
)hhE F
{ii 	
ifjj 
(jj 
dtojj 
==jj 
nulljj 
)jj 
throwkk 
newkk 
	Exceptionkk #
(kk# $
$strkk$ P
)kkP Q
;kkQ R
ifmm 
(mm 
stringmm 
.mm 
IsNullOrWhiteSpacemm )
(mm) *
dtomm* -
.mm- .
Nombremm. 4
)mm4 5
)mm5 6
thrownn 
newnn 
	Exceptionnn #
(nn# $
$strnn$ N
)nnN O
;nnO P
ifpp 
(pp 
dtopp 
.pp 
	Duraci√≥npp 
<=pp 
$numpp  !
)pp! "
throwqq 
newqq 
	Exceptionqq #
(qq# $
$strqq$ D
)qqD E
;qqE F
ifss 
(ss 
dtoss 
.ss 
Precioss 
<ss 
$numss 
)ss 
throwtt 
newtt 
	Exceptiontt #
(tt# $
$strtt$ E
)ttE F
;ttF G
varvv 

gimnasioIdvv 
=vv 
_userContextvv )
.vv) *
GetGimnasioIdvv* 7
(vv7 8
)vv8 9
;vv9 :
	Membresiaxx 
	membresiaxx 
;xx  
ifyy 
(yy 
_userContextyy 
.yy 
EsSuperAdminyy )
(yy) *
)yy* +
)yy+ ,
	membresiazz 
=zz 
awaitzz ! 
_membresiaRepositoryzz" 6
.zz6 7
ObtenerPorIdAsynczz7 H
(zzH I
dtozzI L
.zzL M
IdzzM O
)zzO P
;zzP Q
else{{ 
	membresia|| 
=|| 
await|| ! 
_membresiaRepository||" 6
.||6 7)
ObtenerPorIdValidandoGimnasio||7 T
(||T U
dto||U X
.||X Y
Id||Y [
,||[ \

gimnasioId||] g
)||g h
;||h i
if~~ 
(~~ 
	membresia~~ 
==~~ 
null~~ !
)~~! "
throw 
new 
	Exception #
(# $
$str$ a
)a b
;b c
if
ÇÇ 
(
ÇÇ 
_userContext
ÇÇ 
.
ÇÇ 
EsAdminGimnasio
ÇÇ ,
(
ÇÇ, -
)
ÇÇ- .
&&
ÇÇ/ 1
dto
ÇÇ2 5
.
ÇÇ5 6

GimnasioId
ÇÇ6 @
!=
ÇÇA C
	membresia
ÇÇD M
.
ÇÇM N

GimnasioId
ÇÇN X
)
ÇÇX Y
throw
ÉÉ 
new
ÉÉ 
	Exception
ÉÉ #
(
ÉÉ# $
$str
ÉÉ$ [
)
ÉÉ[ \
;
ÉÉ\ ]
	membresia
ÖÖ 
.
ÖÖ 

actualizar
ÖÖ  
(
ÖÖ  !
dto
ÜÜ 
.
ÜÜ 
Nombre
ÜÜ 
,
ÜÜ 
dto
áá 
.
áá 
	Duraci√≥n
áá 
,
áá 
dto
àà 
.
àà 
Precio
àà 
,
àà 
dto
ââ 
.
ââ "
MaximoIngresosPorDia
ââ (
,
ââ( )
dto
ää 
.
ää %
MaximoIngresosPorSemana
ää +
,
ää+ ,
dto
ãã 
.
ãã #
MaximoIngresosTotales
ãã )
)
åå 
;
åå 
return
éé 
await
éé "
_membresiaRepository
éé -
.
éé- .
ActualizarAsync
éé. =
(
éé= >
	membresia
éé> G
)
ééG H
;
ééH I
}
èè 	
public
îî 
async
îî 
Task
îî 
Eliminar
îî "
(
îî" #
int
îî# &
id
îî' )
)
îî) *
{
ïï 	
var
ññ 

gimnasioId
ññ 
=
ññ 
_userContext
ññ )
.
ññ) *
GetGimnasioId
ññ* 7
(
ññ7 8
)
ññ8 9
;
ññ9 :
	Membresia
òò 
	membresia
òò 
;
òò  
if
ôô 
(
ôô 
_userContext
ôô 
.
ôô 
EsSuperAdmin
ôô )
(
ôô) *
)
ôô* +
)
ôô+ ,
	membresia
öö 
=
öö 
await
öö !"
_membresiaRepository
öö" 6
.
öö6 7
ObtenerPorIdAsync
öö7 H
(
ööH I
id
ööI K
)
ööK L
;
ööL M
else
õõ 
	membresia
úú 
=
úú 
await
úú !"
_membresiaRepository
úú" 6
.
úú6 7+
ObtenerPorIdValidandoGimnasio
úú7 T
(
úúT U
id
úúU W
,
úúW X

gimnasioId
úúY c
)
úúc d
;
úúd e
if
ûû 
(
ûû 
	membresia
ûû 
==
ûû 
null
ûû !
)
ûû! "
throw
üü 
new
üü 
	Exception
üü #
(
üü# $
$str
üü$ _
)
üü_ `
;
üü` a
await
°° "
_membresiaRepository
°° &
.
°°& '
EliminarAsync
°°' 4
(
°°4 5
id
°°5 7
)
°°7 8
;
°°8 9
}
¢¢ 	
}
££ 
}§§ øM
ÑC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Application\CasosUso\CRUDGimnasio\GimnasioService.cs
	namespace 	

ControlFit
 
. 
Application  
.  !
CasosUso! )
.) *
CRUDGimnasio* 6
{ 
public 

class 
GimnasioService  
{ 
private 
readonly 
IGimnasioRepository ,
_repo- 2
;2 3
private 
readonly 
IUserContextService ,
_userContext- 9
;9 :
public 
GimnasioService 
( 
IGimnasioRepository 2
repo3 7
,7 8
IUserContextService9 L
userContextM X
)X Y
{ 	
_repo 
= 
repo 
; 
_userContext 
= 
userContext &
;& '
} 	
public 
async 
Task 
< 
Gimnasio "
>" #
CrearGimnasio$ 1
(1 2
GimnasioCrearDTO2 B
dtoC F
)F G
{ 	
if   
(   
_userContext   
.   
EsAdminGimnasio   ,
(  , -
)  - .
)  . /
throw!! 
new!! 
	Exception!! #
(!!# $
$str!!$ l
)!!l m
;!!m n
if## 
(## 
dto## 
==## 
null## 
)## 
throw$$ 
new$$ 
	Exception$$ #
($$# $
$str$$$ M
)$$M N
;$$N O
if&& 
(&& 
string&& 
.&& 
IsNullOrWhiteSpace&& )
(&&) *
dto&&* -
.&&- .
Nombre&&. 4
)&&4 5
)&&5 6
throw'' 
new'' 
	Exception'' #
(''# $
$str''$ >
)''> ?
;''? @
if)) 
()) 
string)) 
.)) 
IsNullOrWhiteSpace)) )
())) *
dto))* -
.))- .
	Direccion)). 7
)))7 8
)))8 9
throw** 
new** 
	Exception** #
(**# $
$str**$ A
)**A B
;**B C
var,, 
	existente,, 
=,, 
await,, !
_repo,," '
.,,' (!
ObtenerPorNombreAsync,,( =
(,,= >
dto,,> A
.,,A B
Nombre,,B H
),,H I
;,,I J
if-- 
(-- 
	existente-- 
==-- 
true-- !
)--! "
throw.. 
new.. 
	Exception.. #
(..# $
$str..$ J
)..J K
;..K L
var00 
gimnasio00 
=00 
new00 
Gimnasio00 '
(00' (
dto00( +
.00+ ,
Nombre00, 2
,002 3
dto004 7
.007 8
	Direccion008 A
,00A B
dto00C F
.00F G
Estado00G M
)00M N
;00N O
return22 
await22 
_repo22 
.22 

CrearAsync22 )
(22) *
gimnasio22* 2
)222 3
;223 4
}33 	
public:: 
async:: 
Task:: 
<:: 
Gimnasio:: "
>::" #
BuscarPorId::$ /
(::/ 0
int::0 3
id::4 6
)::6 7
{;; 	
if<< 
(<< 
id<< 
<=<< 
$num<< 
)<< 
throw== 
new== 
	Exception== #
(==# $
$str==$ 1
)==1 2
;==2 3
if@@ 
(@@ 
_userContext@@ 
.@@ 
EsAdminGimnasio@@ ,
(@@, -
)@@- .
&&@@/ 1
_userContext@@2 >
.@@> ?
GetGimnasioId@@? L
(@@L M
)@@M N
!=@@O Q
id@@R T
)@@T U
throwAA 
newAA 
	ExceptionAA #
(AA# $
$strAA$ S
)AAS T
;AAT U
varCC 
gimnasioCC 
=CC 
awaitCC  
_repoCC! &
.CC& '
ObtenerPorIdAsyncCC' 8
(CC8 9
idCC9 ;
)CC; <
;CC< =
ifEE 
(EE 
gimnasioEE 
==EE 
nullEE  
)EE  !
throwFF 
newFF 
	ExceptionFF #
(FF# $
$strFF$ ;
)FF; <
;FF< =
returnHH 
gimnasioHH 
;HH 
}II 	
publicPP 
asyncPP 
TaskPP 
<PP 
ListPP 
<PP 
GimnasioPP '
>PP' (
>PP( )
BuscarTodosPP* 5
(PP5 6
)PP6 7
{QQ 	
varRR 
todosRR 
=RR 
awaitRR 
_repoRR #
.RR# $
ListarTodosRR$ /
(RR/ 0
)RR0 1
;RR1 2
ifUU 
(UU 
_userContextUU 
.UU 
EsAdminGimnasioUU ,
(UU, -
)UU- .
)UU. /
{VV 
varWW 

gimnasioIdWW 
=WW  
_userContextWW! -
.WW- .
GetGimnasioIdWW. ;
(WW; <
)WW< =
;WW= >
returnXX 
todosXX 
.XX 
WhereXX "
(XX" #
gXX# $
=>XX% '
gXX( )
!=XX* ,
nullXX- 1
&&XX2 4
gXX5 6
.XX6 7
IdXX7 9
==XX: <

gimnasioIdXX= G
)XXG H
.XXH I
ToListXXI O
(XXO P
)XXP Q
;XXQ R
}YY 
return[[ 
todos[[ 
;[[ 
}\\ 	
publiccc 
asynccc 
Taskcc 
<cc 
boolcc 
>cc 
ActualizarGimnasiocc  2
(cc2 3!
GimnasioActualizarDTOcc3 H
dtoccI L
)ccL M
{dd 	
ifff 
(ff 
_userContextff 
.ff 
EsAdminGimnasioff ,
(ff, -
)ff- .
)ff. /
throwgg 
newgg 
	Exceptiongg #
(gg# $
$strgg$ m
)ggm n
;ggn o
ifii 
(ii 
dtoii 
==ii 
nullii 
)ii 
throwjj 
newjj 
	Exceptionjj #
(jj# $
$strjj$ 5
)jj5 6
;jj6 7
varll 
gimnasioll 
=ll 
awaitll  
_repoll! &
.ll& '
ObtenerPorIdAsyncll' 8
(ll8 9
dtoll9 <
.ll< =
Idll= ?
)ll? @
;ll@ A
ifnn 
(nn 
gimnasionn 
==nn 
nullnn  
)nn  !
throwoo 
newoo 
	Exceptionoo #
(oo# $
$stroo$ ;
)oo; <
;oo< =
ifqq 
(qq 
stringqq 
.qq 
IsNullOrWhiteSpaceqq )
(qq) *
dtoqq* -
.qq- .
Nombreqq. 4
)qq4 5
)qq5 6
throwrr 
newrr 
	Exceptionrr #
(rr# $
$strrr$ >
)rr> ?
;rr? @
iftt 
(tt 
stringtt 
.tt 
IsNullOrWhiteSpacett )
(tt) *
dtott* -
.tt- .
	Direcciontt. 7
)tt7 8
)tt8 9
throwuu 
newuu 
	Exceptionuu #
(uu# $
$struu$ A
)uuA B
;uuB C
varww 
	duplicadoww 
=ww 
awaitww !
_repoww" '
.ww' (!
ObtenerPorNombreAsyncww( =
(ww= >
dtoww> A
.wwA B
NombrewwB H
,wwH I
dtowwJ M
.wwM N
IdwwN P
)wwP Q
;wwQ R
ifxx 
(xx 
	duplicadoxx 
==xx 
truexx !
)xx! "
throwyy 
newyy 
	Exceptionyy #
(yy# $
$stryy$ L
)yyL M
;yyM N
gimnasio{{ 
.{{ 

Actualizar{{ 
({{  
dto{{  #
.{{# $
Nombre{{$ *
,{{* +
dto{{, /
.{{/ 0
	Direccion{{0 9
){{9 :
;{{: ;
gimnasio|| 
.|| 
EstablecerEstado|| %
(||% &
dto||& )
.||) *
Estado||* 0
)||0 1
;||1 2
return~~ 
await~~ 
_repo~~ 
.~~ 
ActualizarAsync~~ .
(~~. /
gimnasio~~/ 7
)~~7 8
;~~8 9
} 	
public
ÖÖ 
async
ÖÖ 
Task
ÖÖ 
EliminarGimnasio
ÖÖ *
(
ÖÖ* +
int
ÖÖ+ .
id
ÖÖ/ 1
)
ÖÖ1 2
{
ÜÜ 	
if
àà 
(
àà 
_userContext
àà 
.
àà 
EsAdminGimnasio
àà ,
(
àà, -
)
àà- .
)
àà. /
throw
ââ 
new
ââ 
	Exception
ââ #
(
ââ# $
$str
ââ$ o
)
ââo p
;
ââp q
var
ãã 
gimnasio
ãã 
=
ãã 
await
ãã  
_repo
ãã! &
.
ãã& '
ObtenerPorIdAsync
ãã' 8
(
ãã8 9
id
ãã9 ;
)
ãã; <
;
ãã< =
if
åå 
(
åå 
gimnasio
åå 
==
åå 
null
åå  
)
åå  !
throw
çç 
new
çç 
	Exception
çç #
(
çç# $
$str
çç$ ;
)
çç; <
;
çç< =
await
èè 
_repo
èè 
.
èè 
EliminarAsync
èè %
(
èè% &
id
èè& (
)
èè( )
;
èè) *
}
êê 	
}
ëë 
}íí Ö-
ëC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Application\CasosUso\CRUDAsignacion\AsignacionMembresiaService.cs
	namespace

 	

ControlFit


 
.

 
Application

  
.

  !
CasosUso

! )
.

) *
CRUDAsignacion

* 8
{ 
public 

class &
AsignacionMembresiaService +
{ 
private 
readonly *
IAsignacionMembresiaRepository 7
_repo8 =
;= >
private 
readonly  
IMembresiaRepository -
_membresiaRepo. <
;< =
private 
readonly 
IMiembroRepository +
_miembroRepo, 8
;8 9
public &
AsignacionMembresiaService )
() **
IAsignacionMembresiaRepository *
repo+ /
,/ 0 
IMembresiaRepository  
membresiaRepo! .
,. /
IMiembroRepository 
miembroRepo *
)* +
{ 	
_repo 
= 
repo 
; 
_membresiaRepo 
= 
membresiaRepo *
;* +
_miembroRepo 
= 
miembroRepo &
;& '
} 	
public 
async 
Task 
< 
AsignacionMembresia -
>- .
Crear/ 4
(4 5'
AsignacionMembresiaCrearDTO '
dto( +
)+ ,
{ 	
var 
miembro 
= 
await 
_miembroRepo  ,
., -
ObtenerPorIdAsync- >
(> ?
dto? B
.B C
	MiembroIdC L
)L M
;M N
if!! 
(!! 
miembro!! 
==!! 
null!! 
)!!  
{"" 
throw## 
new## 
	Exception## #
(### $
$str##$ ;
)##; <
;##< =
}$$ 
var&& 
	membresia&& 
=&& 
await&& !
_membresiaRepo&&" 0
.&&0 1
ObtenerPorIdAsync&&1 B
(&&B C
dto&&C F
.&&F G
MembresiaId&&G R
)&&R S
;&&S T
if(( 
((( 
	membresia(( 
==(( 
null(( !
)((! "
{)) 
throw** 
new** 
	Exception** #
(**# $
$str**$ =
)**= >
;**> ?
}++ 
var-- 
asignacionActiva--  
=--! "
await.. 
_repo.. 
... 
ObtenerActivaAsync.. .
(... /
dto../ 2
...2 3
	MiembroId..3 <
)..< =
;..= >
if00 
(00 
asignacionActiva00  
!=00! #
null00$ (
)00( )
{11 
throw22 
new22 
	Exception22 #
(22# $
$str33 ?
)33? @
;33@ A
}44 
var66 

asignacion66 
=66 
new77 
AsignacionMembresia77 '
(77' (
dto88 
.88 
	MiembroId88 !
,88! "
	membresia99 
)99 
;99 
return;; 
await;; 
_repo;; 
.;; 

CrearAsync;; )
(;;) *

asignacion;;* 4
);;4 5
;;;5 6
}<< 	
public>> 
async>> 
Task>> 
<>> 
List>> 
<>> 
AsignacionMembresia>> 2
>>>2 3
>>>3 4
ObtenerTodos>>5 A
(>>A B
)>>B C
{?? 	
return@@ 
await@@ 
_repo@@ 
.@@ 
ObtenerTodosAsync@@ 0
(@@0 1
)@@1 2
;@@2 3
}AA 	
publicCC 
asyncCC 
TaskCC 
<CC 
AsignacionMembresiaCC -
>CC- .
ObtenerPorIdCC/ ;
(CC; <
intCC< ?
idCC@ B
)CCB C
{DD 	
varEE 

asignacionEE 
=EE 
awaitEE "
_repoEE# (
.EE( )
ObtenerPorIdAsyncEE) :
(EE: ;
idEE; =
)EE= >
;EE> ?
ifGG 
(GG 

asignacionGG 
==GG 
nullGG "
)GG" #
{HH 
throwII 
newII 
	ExceptionII #
(II# $
$strII$ >
)II> ?
;II? @
}JJ 
returnLL 

asignacionLL 
;LL 
}MM 	
publicOO 
asyncOO 
TaskOO 
EliminarOO "
(OO" #
intOO# &
idOO' )
)OO) *
{PP 	
varQQ 

asignacionQQ 
=QQ 
awaitQQ "
_repoQQ# (
.QQ( )
ObtenerPorIdAsyncQQ) :
(QQ: ;
idQQ; =
)QQ= >
;QQ> ?
ifSS 
(SS 

asignacionSS 
==SS 
nullSS "
)SS" #
{TT 
throwUU 
newUU 
	ExceptionUU #
(UU# $
$strUU$ >
)UU> ?
;UU? @
}VV 
varXX 
	eliminadoXX 
=XX 
awaitXX !
_repoXX" '
.XX' (
EliminarAsyncXX( 5
(XX5 6
idXX6 8
)XX8 9
;XX9 :
ifZZ 
(ZZ 
!ZZ 
	eliminadoZZ 
)ZZ 
{[[ 
throw\\ 
new\\ 
	Exception\\ #
(\\# $
$str]] <
)]]< =
;]]= >
}^^ 
}__ 	
}`` 
}aa û
ÉC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Application\CasosUso\Auth\RegistrarAdministrador.cs
	namespace

 	

ControlFit


 
.

 
Application

  
.

  !
CasosUso

! )
.

) *
Auth

* .
{ 
public 

class "
RegistrarAdministrador '
{ 
private 
readonly $
IAdministradorRepository 1
_repo2 7
;7 8
public "
RegistrarAdministrador %
(% &$
IAdministradorRepository& >
repo? C
)C D
{ 	
_repo 
= 
repo 
; 
} 	
public!! 
async!! 
Task!! "
EjecutarAsyncRegistrar!! 0
(!!0 1$
RegistroAdministradorDTO!!1 I
dto!!J M
)!!M N
{"" 	
if## 
(## 
string## 
.## 
IsNullOrWhiteSpace## )
(##) *
dto##* -
.##- .
nombreCompleto##. <
)##< =
)##= >
throw$$ 
new$$ 
	Exception$$ #
($$# $
$str$$$ G
)$$G H
;$$H I
if&& 
(&& 
string&& 
.&& 
IsNullOrWhiteSpace&& )
(&&) *
dto&&* -
.&&- .
correo&&. 4
)&&4 5
)&&5 6
throw'' 
new'' 
	Exception'' #
(''# $
$str''$ >
)''> ?
;''? @
if)) 
()) 
string)) 
.)) 
IsNullOrWhiteSpace)) )
())) *
dto))* -
.))- .

contrasena)). 8
)))8 9
||)): <
dto))= @
.))@ A

contrasena))A K
.))K L
Length))L R
<))S T
$num))U V
)))V W
throw** 
new** 
	Exception** #
(**# $
$str**$ T
)**T U
;**U V
var-- 
	existente-- 
=-- 
await-- !
_repo--" '
.--' (!
ObtenerPorCorreoAsync--( =
(--= >
dto--> A
.--A B
correo--B H
)--H I
;--I J
if.. 
(.. 
	existente.. 
!=.. 
null.. !
)..! "
throw// 
new// 
	Exception// #
(//# $
$str//$ B
)//B C
;//C D
var11 
admin11 
=11 
new11 
Administrador11 )
(11) *
dto22 
.22 
nombreCompleto22 "
,22" #
dto33 
.33 
correo33 
,33 
$str44 
,44 
dto55 
.55 

GimnasioId55 
)66 
;66 
admin88 
.88 
AsignarContrasena88 #
(88# $
dto88$ '
.88' (

contrasena88( 2
)882 3
;883 4
await99 
_repo99 
.99 
GuardarAsync99 $
(99$ %
admin99% *
)99* +
;99+ ,
}:: 	
};; 
}<< ∑
C:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Application\CasosUso\Auth\LoginAdministrador.cs
	namespace

 	

ControlFit


 
.

 
Application

  
.

  !
CasosUso

! )
.

) *
Auth

* .
{ 
public 

class 
LoginAdministrador #
{ 
private 
readonly $
IAdministradorRepository 1
_repo2 7
;7 8
private 
readonly 
ITokenService &
_tokenService' 4
;4 5
public 
LoginAdministrador !
(! "$
IAdministradorRepository" :
repo; ?
,? @
ITokenServiceA N
tokenServiceO [
)[ \
{ 	
_repo 
= 
repo 
; 
_tokenService 
= 
tokenService (
;( )
} 	
public 
async 
Task 
< 
string  
?  !
>! "
EjecutarAsyncLogin# 5
(5 6!
LoginAdministradorDTO6 K
dtoL O
)O P
{   	
var!! 
administrador!! 
=!! 
await!!  %
_repo!!& +
.!!+ ,!
ObtenerPorCorreoAsync!!, A
(!!A B
dto!!B E
.!!E F
correo!!F L
)!!L M
;!!M N
if"" 
("" 
administrador"" 
==""  
null""! %
||""& (
!"") *
administrador""* 7
.""7 8
ValidarPassword""8 G
(""G H
dto""H K
.""K L

contrasena""L V
)""V W
)""W X
return## 
null## 
;## 
return&& 
_tokenService&&  
.&&  !
GenerarToken&&! -
(&&- .
administrador&&. ;
.&&; <
Id&&< >
,&&> ?
administrador&&@ M
.&&M N
Correo&&N T
,&&T U
administrador&&V c
.&&c d

GimnasioId&&d n
)&&n o
;&&o p
}'' 	
}(( 
})) 