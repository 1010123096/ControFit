±
yC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Infrastructure\Repository\TokenService.cs
	namespace 	

ControlFit
 
. 
Infrastructure #
.# $

Repository$ .
{ 
public 

class 
TokenService 
: 
ITokenService  -
{ 
private 
readonly 
IConfiguration '
_config( /
;/ 0
public 
TokenService 
( 
IConfiguration *
config+ 1
)1 2
{ 	
_config 
= 
config 
; 
} 	
public 
string 
GenerarToken "
(" #
int# &
administradorId' 6
,6 7
string8 >
correo? E
,E F
intG J
?J K

gimnasioIdL V
=W X
nullY ]
)] ^
{ 	
var 
claims 
= 
new 
List !
<! "
Claim" '
>' (
{ 
new 
Claim 
( #
JwtRegisteredClaimNames 1
.1 2
Sub2 5
,5 6
administradorId7 F
.F G
ToStringG O
(O P
)P Q
)Q R
,R S
new 
Claim 
( #
JwtRegisteredClaimNames 1
.1 2
Email2 7
,7 8
correo9 ?
)? @
,@ A
new 
Claim 
( 

ClaimTypes $
.$ %
Role% )
,) *
$str+ :
): ;
,; <
new   
Claim   
(   
$str   &
,  & '
(  ( )

gimnasioId  ) 3
??  4 6
$num  7 8
)  8 9
.  9 :
ToString  : B
(  B C
)  C D
)  D E
}!! 
;!! 
var## 
key## 
=## 
new##  
SymmetricSecurityKey## .
(##. /
Encoding##/ 7
.##7 8
UTF8##8 <
.##< =
GetBytes##= E
(##E F
_config##F M
[##M N
$str##N W
]##W X
)##X Y
)##Y Z
;##Z [
var$$ 
creds$$ 
=$$ 
new$$ 
SigningCredentials$$ .
($$. /
key$$/ 2
,$$2 3
SecurityAlgorithms$$4 F
.$$F G

HmacSha256$$G Q
)$$Q R
;$$R S
var&& 
token&& 
=&& 
new&& 
JwtSecurityToken&& ,
(&&, -
issuer'' 
:'' 
_config'' 
[''  
$str''  ,
]'', -
,''- .
audience(( 
:(( 
_config(( !
[((! "
$str((" 0
]((0 1
,((1 2
claims)) 
:)) 
claims)) 
,)) 
expires** 
:** 
DateTime** !
.**! "
UtcNow**" (
.**( )
AddHours**) 1
(**1 2
$num**2 3
)**3 4
,**4 5
signingCredentials++ "
:++" #
creds++$ )
),, 
;,, 
return.. 
new.. #
JwtSecurityTokenHandler.. .
(... /
)../ 0
...0 1

WriteToken..1 ;
(..; <
token..< A
)..A B
;..B C
}// 	
}00 
}11 €(
~C:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Infrastructure\Repository\MiembroRepository.cs
	namespace 	

ControlFit
 
. 
Infrastructure #
.# $
Repositorys$ /
{ 
public 

class 
MiembroRepository "
:# $
IMiembroRepository% 7
{ 
private 
readonly 
AppDbContext %
_context& .
;. /
public 
MiembroRepository  
(  !
AppDbContext! -
context. 5
)5 6
{ 	
_context 
= 
context 
; 
} 	
public 
async 
Task 
< 
bool 
> 
ActualizarAsync  /
(/ 0
Miembro0 7
miembro8 ?
)? @
{ 	
_context 
. 
Miembros 
. 
Update $
($ %
miembro% ,
), -
;- .
return 
await 
_context "
." #
SaveChangesAsync# 3
(3 4
)4 5
>6 7
$num8 9
;9 :
} 	
public 
async 
Task 
< 
Miembro !
>! "

CrearAsync# -
(- .
Miembro. 5
miembro6 =
)= >
{ 	
await 
_context 
. 
AddAsync "
(" #
miembro# *
)* +
;+ ,
await 
_context 
. 
SaveChangesAsync +
(+ ,
), -
;- .
return   
miembro   
;   
}"" 	
public$$ 
async$$ 
Task$$ 
EliminarAsync$$ '
($$' (
int$$( +
id$$, .
)$$. /
{%% 	
var&& 
miembro&& 
=&& 
await&& 
_context&&  (
.&&( )
Miembros&&) 1
.&&1 2
	FindAsync&&2 ;
(&&; <
id&&< >
)&&> ?
;&&? @
if'' 
('' 
miembro'' 
!='' 
null'' 
)''  
{(( 
_context)) 
.)) 
Miembros)) !
.))! "
Remove))" (
())( )
miembro))) 0
)))0 1
;))1 2
await** 
_context** 
.** 
SaveChangesAsync** /
(**/ 0
)**0 1
;**1 2
}++ 
},, 	
public.. 
async.. 
Task.. 
<.. 
List.. 
<.. 
Miembro.. &
>..& '
>..' (
ListarTodos..) 4
(..4 5
)..5 6
{// 	
return11 
await11 
_context11 !
.11! "
Miembros11" *
.11* +
AsNoTracking11+ 7
(117 8
)118 9
.119 :
ToListAsync11: E
(11E F
)11F G
;11G H
}44 	
public66 
async66 
Task66 
<66 
List66 
<66 
Miembro66 &
>66& '
>66' ("
ListarTodosPorGimnasio66) ?
(66? @
int66@ C

gimnasioId66D N
)66N O
{77 	
return88 
await88 
_context88 !
.88! "
Miembros88" *
.99 
AsNoTracking99 
(99 
)99 
.:: 
Where:: 
(:: 
x:: 
=>:: 
x:: 
.:: 

GimnasioId:: (
==::) +

gimnasioId::, 6
)::6 7
.;; 
ToListAsync;; 
(;; 
);; 
;;; 
}<< 	
public>> 
async>> 
Task>> 
<>> 
Miembro>> !
?>>! "
>>>" #
ObtenerPorIdAsync>>$ 5
(>>5 6
int>>6 9
id>>: <
)>>< =
{?? 	
return@@ 
await@@ 
_context@@ !
.@@! "
Miembros@@" *
.@@* +
	FindAsync@@+ 4
(@@4 5
id@@5 7
)@@7 8
;@@8 9
}AA 	
publicCC 
asyncCC 
TaskCC 
<CC 
MiembroCC !
?CC! "
>CC" #)
ObtenerPorIdValidandoGimnasioCC$ A
(CCA B
intCCB E
idCCF H
,CCH I
intCCJ M

gimnasioIdCCN X
)CCX Y
{DD 	
returnEE 
awaitEE 
_contextEE !
.EE! "
MiembrosEE" *
.FF 
AsNoTrackingFF 
(FF 
)FF 
.GG 
FirstOrDefaultAsyncGG $
(GG$ %
xGG% &
=>GG' )
xGG* +
.GG+ ,
IdGG, .
==GG/ 1
idGG2 4
&&GG5 7
xGG8 9
.GG9 :

GimnasioIdGG: D
==GGE G

gimnasioIdGGH R
)GGR S
;GGS T
}HH 	
}JJ 
}KK ˝3
ÄC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Infrastructure\Repository\MembresiaRepository.cs
	namespace 	

ControlFit
 
. 
Infrastructure #
.# $

Repository$ .
{ 
public 

class 
MembresiaRepository $
:% & 
IMembresiaRepository' ;
{ 
private 
readonly 
AppDbContext %
_appDbContext& 3
;3 4
public 
MembresiaRepository "
(" #
AppDbContext# /
appDbContext0 <
)< =
{ 	
_appDbContext 
= 
appDbContext (
;( )
} 	
public 
async 
Task 
< 
bool 
> 
ActualizarAsync  /
(/ 0
	Membresia0 9
	membresia: C
)C D
{ 	
var 
membresiaExistente "
=# $
await% *
_appDbContext+ 8
.8 9

Membresias9 C
. 	
FirstOrDefaultAsync	 
( 
m 
=> !
m" #
.# $
Nombre$ *
==+ -
	membresia. 7
.7 8
Nombre8 >
)> ?
;? @
if 
( 
membresiaExistente "
==# %
null& *
)* +
{ 
throw 
new 
	Exception #
(# $
$str$ M
)M N
;N O
} 
_appDbContext 
. 

Membresias %
.% &
Update& ,
(, -
	membresia- 6
)6 7
;7 8
return 
await 
_appDbContext &
.& '
SaveChangesAsync' 7
(7 8
)8 9
>: ;
$num< =
;= >
}!! 	
public## 
async## 
Task## 
<## 
	Membresia## #
>### $

CrearAsync##% /
(##/ 0
	Membresia##0 9
	membresia##: C
)##C D
{$$ 	
var%% 
membresiaExistente%% "
=%%# $
await%%% *
_appDbContext%%+ 8
.%%8 9

Membresias%%9 C
.&& 	
FirstOrDefaultAsync&&	 
(&& 
m&& 
=>&& !
m&&" #
.&&# $
Nombre&&$ *
==&&+ -
	membresia&&. 7
.&&7 8
Nombre&&8 >
)&&> ?
;&&? @
if'' 
('' 
membresiaExistente'' "
!=''# %
null''& *
)''* +
{(( 
throw)) 
new)) 
	Exception)) #
())# $
$str))$ M
)))M N
;))N O
}** 
await++ 
_appDbContext++ 
.++  

Membresias++  *
.++* +
AddAsync+++ 3
(++3 4
	membresia++4 =
)++= >
;++> ?
await,, 
_appDbContext,, 
.,,  
SaveChangesAsync,,  0
(,,0 1
),,1 2
;,,2 3
return-- 
	membresia-- 
;-- 
}.. 	
public11 
async11 
Task11 
EliminarAsync11 '
(11' (
int11( +
id11, .
)11. /
{22 	
var33 
	membresia33 
=33 
await33 !
_appDbContext33" /
.33/ 0

Membresias330 :
.33: ;
	FindAsync33; D
(33D E
id33E G
)33G H
;33H I
if44 
(44 
	membresia44 
!=44 
null44 !
)44! "
{55 
_appDbContext66 
.66 
Remove66 $
(66$ %
id66% '
)66' (
;66( )
await77 
_appDbContext77 #
.77# $
SaveChangesAsync77$ 4
(774 5
)775 6
;776 7
}88 
}:: 	
public<< 
async<< 
Task<< 
<<< 
List<< 
<<< 
	Membresia<< (
><<( )
><<) *
ListarTodos<<+ 6
(<<6 7
)<<7 8
{== 	
return?? 
await?? 
_appDbContext?? &
.??& '

Membresias??' 1
.??1 2
AsNoTracking??2 >
(??> ?
)??? @
.??@ A
ToListAsync??A L
(??L M
)??M N
;??N O
}@@ 	
publicBB 
asyncBB 
TaskBB 
<BB 
ListBB 
<BB 
	MembresiaBB (
>BB( )
>BB) *"
ListarTodosPorGimnasioBB+ A
(BBA B
intBBB E

gimnasioIdBBF P
)BBP Q
{CC 	
returnDD 
awaitDD 
_appDbContextDD &
.DD& '

MembresiasDD' 1
.EE 
AsNoTrackingEE 
(EE 
)EE 
.FF 
WhereFF 
(FF 
xFF 
=>FF 
xFF 
.FF 

GimnasioIdFF (
==FF) +

gimnasioIdFF, 6
)FF6 7
.GG 
ToListAsyncGG 
(GG 
)GG 
;GG 
}HH 	
publicJJ 
asyncJJ 
TaskJJ 
<JJ 
	MembresiaJJ #
?JJ# $
>JJ$ %
ObtenerPorIdAsyncJJ& 7
(JJ7 8
intJJ8 ;
idJJ< >
)JJ> ?
{KK 	
returnMM 
awaitMM 
_appDbContextMM &
.MM& '

MembresiasMM' 1
.MM1 2
	FindAsyncMM2 ;
(MM; <
idMM< >
)MM> ?
;MM? @
}NN 	
publicPP 
asyncPP 
TaskPP 
<PP 
	MembresiaPP #
?PP# $
>PP$ %)
ObtenerPorIdValidandoGimnasioPP& C
(PPC D
intPPD G
idPPH J
,PPJ K
intPPL O

gimnasioIdPPP Z
)PPZ [
{QQ 	
returnRR 
awaitRR 
_appDbContextRR &
.RR& '

MembresiasRR' 1
.SS 
AsNoTrackingSS 
(SS 
)SS 
.TT 
FirstOrDefaultAsyncTT $
(TT$ %
xTT% &
=>TT' )
xTT* +
.TT+ ,
IdTT, .
==TT/ 1
idTT2 4
&&TT5 7
xTT8 9
.TT9 :

GimnasioIdTT: D
==TTE G

gimnasioIdTTH R
)TTR S
;TTS T
}UU 	
}VV 
}WW Î)
C:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Infrastructure\Repository\GimnasioRepository.cs
	namespace 	

ControlFit
 
. 
Infrastructure #
.# $

Repository$ .
{ 
public 

class 
GimnasioRepository #
:$ %
IGimnasioRepository& 9
{ 
private 
readonly 
AppDbContext %
_context& .
;. /
public 
GimnasioRepository !
(! "
AppDbContext" .
context/ 6
)6 7
{ 	
_context 
= 
context 
; 
} 	
public 
async 
Task 
< 
bool 
>  
ActualizarAsync! 0
(0 1
Gimnasio1 9
gimnasio: B
)B C
{ 	
_context 
. 
	gimnasios 
. 
Update %
(% &
gimnasio& .
). /
;/ 0
return 
await 
_context !
.! "
SaveChangesAsync" 2
(2 3
)3 4
>5 6
$num7 8
;8 9
} 	
public 
async 
Task 
< 
Gimnasio "
>" #

CrearAsync$ .
(. /
Gimnasio/ 7
gimnasio8 @
)@ A
{ 	
_context 
. 
	gimnasios 
. 
Add "
(" #
gimnasio# +
)+ ,
;, -
await 
_context 
. 
SaveChangesAsync +
(+ ,
), -
;- .
return   
gimnasio   
;   
}!! 	
public## 
async## 
Task## 
EliminarAsync## (
(##( )
int##) ,
id##- /
)##/ 0
{$$ 	
var%% 
gimnasio%% 
=%% 
await%%  
_context%%! )
.%%) *
	gimnasios%%* 3
.%%3 4
	FindAsync%%4 =
(%%= >
id%%> @
)%%@ A
;%%A B
if'' 
('' 
gimnasio'' 
!='' 
null''  
)''  !
{(( 
_context)) 
.)) 
	gimnasios)) "
.))" #
Remove))# )
())) *
gimnasio))* 2
)))2 3
;))3 4
await** 
_context** 
.** 
SaveChangesAsync** /
(**/ 0
)**0 1
;**1 2
}++ 
},, 	
public.. 
async.. 
Task.. 
<.. 
List.. 
<.. 
Gimnasio.. '
?..' (
>..( )
>..) *
ListarTodos..+ 6
(..6 7
)..7 8
{// 	
return00 
_context00 
.00 
	gimnasios00 %
.00% &
AsNoTracking00& 2
(002 3
)003 4
.004 5
ToList005 ;
(00; <
)00< =
;00= >
}11 	
public33 
async33 
Task33 !
<33! "
Gimnasio33" *
?33* +
>33+ ,
ObtenerPorIdAsync33- >
(33> ?
int33? B
id33C E
)33E F
{44 	
return55 
await55  
_context55! )
.55) *
	gimnasios55* 3
.553 4
	FindAsync554 =
(55= >
id55> @
)55@ A
;55A B
}66 	
public88 
async88 
Task88 
<88 
bool88 
?88 
>88  !
ObtenerPorNombreAsync88! 6
(886 7
string887 =
nombre88> D
,88D E
int88F I
idActual88J R
)88R S
{99 	
return:: 
await:: 
_context:: !
.::! "
	gimnasios::" +
.;;	 

AnyAsync;;
 
(;; 
g;; 
=>;; 
g;; 
.;; 
Nombre;;  
.;;  !
ToLower;;! (
(;;( )
);;) *
==;;+ -
nombre;;. 4
.;;4 5
Trim;;5 9
(;;9 :
);;: ;
.;;; <
ToLower;;< C
(;;C D
);;D E
&&;;F H
g;;I J
.;;J K
Id;;K M
!=;;N P
idActual;;Q Y
);;Y Z
;;;Z [
}<< 	
public>> 
async>> 
Task>> 
<>> 
bool>> 
?>> 
>>>  !
ObtenerPorNombreAsync>>! 6
(>>6 7
string>>7 =
nombre>>> D
)>>D E
{?? 	
return@@ 
await@@ 
_context@@ !
.@@! "
	gimnasios@@" +
.AA	 

AnyAsyncAA
 
(AA 
gAA 
=>AA 
gAA 
.AA 
NombreAA  
.AA  !
ToLowerAA! (
(AA( )
)AA) *
==AA+ -
nombreAA. 4
.AA4 5
TrimAA5 9
(AA9 :
)AA: ;
.AA; <
ToLowerAA< C
(AAC D
)AAD E
)AAE F
;AAF G
}BB 	
}CC 
}DD ®4
ÅC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Infrastructure\Repository\AsistenciaRepository.cs
	namespace 	

ControlFit
 
. 
Infrastructure #
.# $

Repository$ .
{ 
public 

class  
AsistenciaRepository %
:& '!
IAsistenciaRepository( =
{ 
private 
readonly 
AppDbContext %
_context& .
;. /
public  
AsistenciaRepository #
(# $
AppDbContext$ 0
context1 8
)8 9
{ 	
_context 
= 
context 
; 
} 	
public 
async 
Task 
RegistrarAsync (
(( )

Asistencia) 3

asistencia4 >
)> ?
{ 	
_context 
. 
Asistencias  
.  !
Add! $
($ %

asistencia% /
)/ 0
;0 1
await 
_context 
. 
SaveChangesAsync +
(+ ,
), -
;- .
} 	
public 
async 
Task 
< 
bool 
> 
YaIngresoHoyAsync  1
(1 2
int2 5
	miembroId6 ?
)? @
{ 	
var 
hoy 
= 
DateTime 
. 
Today $
;$ %
var 
manana 
= 
hoy 
. 
AddDays $
($ %
$num% &
)& '
;' (
return!! 
await!! 
_context!! !
.!!! "
Asistencias!!" -
."" 
AsNoTracking"" 
("" 
)"" 
.## 
AnyAsync## 
(## 
x## 
=>## 
x##  
.##  !
	MiembroId##! *
==##+ -
	miembroId##. 7
&&##8 :
x##; <
.##< =
FechaHoraAcceso##= L
>=##M O
hoy##P S
&&##T V
x##W X
.##X Y
FechaHoraAcceso##Y h
<##i j
manana##k q
)##q r
;##r s
}$$ 	
public&& 
async&& 
Task&& 
<&& 
int&& 
>&& &
ObtenerIngresosSemanaAsync&& 9
(&&9 :
int&&: =
	miembroId&&> G
)&&G H
{'' 	
var(( 
inicioSemana(( 
=(( 
DateTime(( '
.((' (
Today((( -
.((- .
AddDays((. 5
(((5 6
-((6 7
(((7 8
(((8 9
int((9 <
)((< =
DateTime((= E
.((E F
Today((F K
.((K L
	DayOfWeek((L U
)((U V
)((V W
;((W X
return** 
await** 
_context** !
.**! "
Asistencias**" -
.++ 
AsNoTracking++ 
(++ 
)++ 
.,, 

CountAsync,, 
(,, 
x,, 
=>,,  
x,,! "
.,," #
	MiembroId,,# ,
==,,- /
	miembroId,,0 9
&&,,: <
x,,= >
.,,> ?
FechaHoraAcceso,,? N
>=,,O Q
inicioSemana,,R ^
),,^ _
;,,_ `
}-- 	
public// 
async// 
Task// 
<// 
List// 
<// 

Asistencia// )
>//) *
>//* +
ObtenerTodosAsync//, =
(//= >
)//> ?
{00 	
return11 
await11 
_context11 !
.11! "
Asistencias11" -
.22 
AsNoTracking22 
(22 
)22 
.33 
ToListAsync33 
(33 
)33 
;33 
}44 	
public66 
async66 
Task66 
<66 
List66 
<66 

Asistencia66 )
>66) *
>66* +#
ObtenerTodosPorGimnasio66, C
(66C D
int66D G

gimnasioId66H R
)66R S
{77 	
return88 
await88 
(88 
from88 

asistencia88 )
in88* ,
_context88- 5
.885 6
Asistencias886 A
join99 
miembro99 &
in99' )
_context99* 2
.992 3
Miembros993 ;
on99< >

asistencia99? I
.99I J
	MiembroId99J S
equals99T Z
miembro99[ b
.99b c
Id99c e
where:: 
miembro::  '
.::' (

GimnasioId::( 2
==::3 5

gimnasioId::6 @
select;;  

asistencia;;! +
);;+ ,
.<< 
AsNoTracking<< 
(<< 
)<< 
.== 
ToListAsync== 
(== 
)== 
;== 
}>> 	
public@@ 
async@@ 
Task@@ 
<@@ 
List@@ 
<@@ 

Asistencia@@ )
>@@) *
>@@* +$
ObtenerPorMiembroEnRango@@, D
(@@D E
int@@E H
	miembroId@@I R
,@@R S
DateTime@@T \
fechaInicio@@] h
,@@h i
DateTime@@j r
fechaFin@@s {
,@@{ |
int	@@} Ä

gimnasioId
@@Å ã
)
@@ã å
{AA 	
returnBB 
awaitBB 
(BB 
fromBB 

asistenciaBB )
inBB* ,
_contextBB- 5
.BB5 6
AsistenciasBB6 A
joinCC 
miembroCC &
inCC' )
_contextCC* 2
.CC2 3
MiembrosCC3 ;
onCC< >

asistenciaCC? I
.CCI J
	MiembroIdCCJ S
equalsCCT Z
miembroCC[ b
.CCb c
IdCCc e
whereDD 

asistenciaDD  *
.DD* +
	MiembroIdDD+ 4
==DD5 7
	miembroIdDD8 A
&&EE  "

asistenciaEE# -
.EE- .
FechaHoraAccesoEE. =
>=EE> @
fechaInicioEEA L
&&FF  "

asistenciaFF# -
.FF- .
FechaHoraAccesoFF. =
<=FF> @
fechaFinFFA I
&&GG  "
miembroGG# *
.GG* +

GimnasioIdGG+ 5
==GG6 8

gimnasioIdGG9 C
selectHH  

asistenciaHH! +
)HH+ ,
.II 
AsNoTrackingII 
(II 
)II 
.JJ 
ToListAsyncJJ 
(JJ 
)JJ 
;JJ 
}KK 	
}LL 
}MM €3
äC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Infrastructure\Repository\AsignacionMembresiaRepository.cs
	namespace 	

ControlFit
 
. 
Infrastructure #
.# $

Repository$ .
{ 
public 

class )
AsignacionMembresiaRepository .
:/ 0*
IAsignacionMembresiaRepository1 O
{ 
private 
readonly 
AppDbContext %
_context& .
;. /
public )
AsignacionMembresiaRepository ,
(, -
AppDbContext- 9
context: A
)A B
{ 	
_context 
= 
context 
; 
} 	
public 
async 
Task 
< 
AsignacionMembresia -
>- .

CrearAsync/ 9
(9 :
AsignacionMembresia 

asignacion  *
)* +
{ 	
_context 
. !
AsignacionesMembresia *
.* +
Add+ .
(. /

asignacion/ 9
)9 :
;: ;
await 
_context 
. 
SaveChangesAsync +
(+ ,
), -
;- .
return 

asignacion 
; 
} 	
public   
async   
Task   
<   
AsignacionMembresia   -
?  - .
>  . /
ObtenerPorIdAsync  0 A
(  A B
int  B E
id  F H
)  H I
{!! 	
return"" 
await"" 
_context"" !
.""! "!
AsignacionesMembresia""" 7
.## 
FirstOrDefaultAsync## $
(##$ %
x##% &
=>##' )
x##* +
.##+ ,
Id##, .
==##/ 1
id##2 4
)##4 5
;##5 6
}$$ 	
public&& 
async&& 
Task&& 
<&& 
List&& 
<&& 
AsignacionMembresia&& 2
>&&2 3
>&&3 4
ObtenerTodosAsync&&5 F
(&&F G
)&&G H
{'' 	
return(( 
await(( 
_context(( !
.((! "!
AsignacionesMembresia((" 7
.)) 
ToListAsync)) 
()) 
))) 
;)) 
}** 	
public,, 
async,, 
Task,, 
<,, 
List,, 
<,, 
AsignacionMembresia,, 2
>,,2 3
>,,3 4#
ObtenerTodosPorGimnasio,,5 L
(,,L M
int,,M P

gimnasioId,,Q [
),,[ \
{-- 	
return.. 
await.. 
(.. 
from.. 

asignacion.. )
in..* ,
_context..- 5
...5 6!
AsignacionesMembresia..6 K
join// 
	membresia// (
in//) +
_context//, 4
.//4 5

Membresias//5 ?
on//@ B

asignacion//C M
.//M N
MembresiaId//N Y
equals//Z `
	membresia//a j
.//j k
Id//k m
where00 
	membresia00  )
.00) *

GimnasioId00* 4
==005 7

gimnasioId008 B
select11  

asignacion11! +
)11+ ,
.22 
ToListAsync22 
(22 
)22 
;22 
}33 	
public55 
async55 
Task55 
<55 
List55 
<55 
AsignacionMembresia55 2
>552 3
>553 4%
ObtenerPorMiembroGimnasio555 N
(55N O
int55O R
	miembroId55S \
,55\ ]
int55^ a

gimnasioId55b l
)55l m
{66 	
return77 
await77 
(77 
from77 

asignacion77 )
in77* ,
_context77- 5
.775 6!
AsignacionesMembresia776 K
join88 
	membresia88 (
in88) +
_context88, 4
.884 5

Membresias885 ?
on88@ B

asignacion88C M
.88M N
MembresiaId88N Y
equals88Z `
	membresia88a j
.88j k
Id88k m
where99 

asignacion99  *
.99* +
	MiembroId99+ 4
==995 7
	miembroId998 A
&&99B D
	membresia99E N
.99N O

GimnasioId99O Y
==99Z \

gimnasioId99] g
select::  

asignacion::! +
)::+ ,
.;; 
ToListAsync;; 
(;; 
);; 
;;; 
}<< 	
public>> 
async>> 
Task>> 
<>> 
bool>> 
>>> 
EliminarAsync>>  -
(>>- .
int>>. 1
id>>2 4
)>>4 5
{?? 	
var@@ 

asignacion@@ 
=@@ 
await@@ "
_context@@# +
.@@+ ,!
AsignacionesMembresia@@, A
.AA 
FirstOrDefaultAsyncAA $
(AA$ %
xAA% &
=>AA' )
xAA* +
.AA+ ,
IdAA, .
==AA/ 1
idAA2 4
)AA4 5
;AA5 6
ifCC 
(CC 

asignacionCC 
==CC 
nullCC "
)CC" #
returnDD 
falseDD 
;DD 
_contextFF 
.FF !
AsignacionesMembresiaFF *
.FF* +
RemoveFF+ 1
(FF1 2

asignacionFF2 <
)FF< =
;FF= >
returnHH 
awaitHH 
_contextHH !
.HH! "
SaveChangesAsyncHH" 2
(HH2 3
)HH3 4
>HH5 6
$numHH7 8
;HH8 9
}II 	
publicKK 
asyncKK 
TaskKK 
<KK 
AsignacionMembresiaKK -
?KK- .
>KK. /
ObtenerActivaAsyncKK0 B
(KKB C
intKKC F
	miembroIdKKG P
)KKP Q
{LL 	
returnMM 
awaitMM 
_contextMM !
.MM! "!
AsignacionesMembresiaMM" 7
.NN 
FirstOrDefaultAsyncNN $
(NN$ %
xNN% &
=>NN' )
xOO 
.OO 
	MiembroIdOO 
==OO  "
	miembroIdOO# ,
&&OO- /
xPP 
.PP 
EstadoPP 
==PP 
truePP  $
&&PP% '
xQQ 
.QQ 
FechaFinQQ 
>=QQ !
DateTimeQQ" *
.QQ* +
TodayQQ+ 0
)QQ0 1
;QQ1 2
}RR 	
}SS 
}TT ª
ÑC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Infrastructure\Repository\AdministradorRepository.cs
	namespace 	

ControlFit
 
. 
Infrastructure #
.# $

Repository$ .
{ 
public 

class #
AdministradorRepository (
:) *$
IAdministradorRepository+ C
{ 
private 
readonly 
AppDbContext %
_context& .
;. /
public #
AdministradorRepository &
(& '
AppDbContext' 3
context4 ;
); <
{ 	
_context 
= 
context 
; 
} 	
public 
async 
Task 
< 
Administrador '
?' (
>( )!
ObtenerPorCorreoAsync* ?
(? @
string@ F
correoG M
)M N
{ 	
return 
await 
_context !
.! "
Administradores" 1
.1 2
FirstOrDefaultAsync2 E
(E F
aF G
=>H J
aK L
.L M
CorreoM S
==T V
correoW ]
)] ^
;^ _
} 	
public 
async 
Task 
GuardarAsync &
(& '
Administrador' 4
admin5 :
): ;
{ 	
_context 
. 
Administradores $
.$ %
Add% (
(( )
admin) .
). /
;/ 0
await 
_context 
. 
SaveChangesAsync +
(+ ,
), -
;- .
} 	
}   
}!! †1
{C:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Infrastructure\Persistencia\AppDbContext.cs
	namespace		 	

ControlFit		
 
.		 
Infrastructure		 #
.		# $
Persistencia		$ 0
{

 
public 

class 
AppDbContext 
: 
	DbContext  )
{ 
public 
AppDbContext 
( 
DbContextOptions ,
<, -
AppDbContext- 9
>9 :
options; B
)B C
:D E
baseF J
(J K
optionsK R
)R S
{T U
}V W
public 
DbSet 
< 
Administrador "
>" #
Administradores$ 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
public 
DbSet 
< 
Miembro 
> 
Miembros &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
DbSet 
< 
Gimnasio 
> 
	gimnasios (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
DbSet 
< 
	Membresia 
> 

Membresias  *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
DbSet 
< 
AsignacionMembresia (
>( )!
AsignacionesMembresia* ?
{@ A
getB E
;E F
setG J
;J K
}L M
public 
DbSet 
< 

Asistencia 
>  
Asistencias! ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
	protected 
override 
void 
OnModelCreating  /
(/ 0
ModelBuilder0 <
modelBuilder= I
)I J
{ 	
modelBuilder 
. 
Entity 
<  
Administrador  -
>- .
(. /
entity/ 5
=>6 8
{ 
entity 
. 
ToTable 
( 
$str .
). /
;/ 0
entity 
. 
HasKey 
( 
a 
=>  "
a# $
.$ %
Id% '
)' (
;( )
entity 
. 
Property 
(  
a  !
=>" $
a% &
.& '
NombreCompleto' 5
)5 6
.6 7
HasMaxLength7 C
(C D
$numD G
)G H
.H I

IsRequiredI S
(S T
)T U
;U V
entity 
. 
Property 
(  
a  !
=>" $
a% &
.& '
Correo' -
)- .
.. /
HasMaxLength/ ;
(; <
$num< ?
)? @
;@ A
entity"" 
."" 
Property"" 
(""  
a""  !
=>""" $
a""% &
.""& '

Contrasena""' 1
)""1 2
.""2 3
HasMaxLength""3 ?
(""? @
$num""@ C
)""C D
.""D E

IsRequired""E O
(""O P
)""P Q
;""Q R
}## 
)## 
;## 
modelBuilder$$ 
.$$ 
Entity$$ 
<$$  
Miembro$$  '
>$$' (
($$( )
entity$$) /
=>$$0 2
{%% 
entity'' 
.'' 
ToTable'' 
('' 
$str'' (
)''( )
;'') *
entity(( 
.(( 
HasKey(( 
((( 
a(( 
=>((  "
a((# $
.(($ %
Id((% '
)((' (
;((( )
}++ 
)++ 
;++ 
modelBuilder,, 
.,, 
Entity,, 
<,,  
Gimnasio,,  (
>,,( )
(,,) *
entity,,* 0
=>,,1 3
{-- 
entity// 
.// 
ToTable// 
(// 
$str// )
)//) *
;//* +
entity00 
.00 
HasKey00 
(00 
a00 
=>00  "
a00# $
.00$ %
Id00% '
)00' (
;00( )
}33 
)33 
;33 
modelBuilder44 
.44 
Entity44 
<44  
	Membresia44  )
>44) *
(44* +
entity44+ 1
=>442 4
{55 
entity77 
.77 
ToTable77 
(77 
$str77 *
)77* +
;77+ ,
entity88 
.88 
HasKey88 
(88 
a88 
=>88  "
a88# $
.88$ %
Id88% '
)88' (
;88( )
};; 
);; 
;;; 
modelBuilder<< 
.<< 
Entity<< 
<<<  
AsignacionMembresia<<  3
><<3 4
(<<4 5
entity<<5 ;
=><<< >
{== 
entity?? 
.?? 
ToTable?? 
(?? 
$str?? +
)??+ ,
;??, -
entity@@ 
.@@ 
HasKey@@ 
(@@ 
a@@ 
=>@@  "
a@@# $
.@@$ %
Id@@% '
)@@' (
;@@( )
}CC 
)CC 
;CC 
modelBuilderEE 
.EE 
EntityEE 
<EE  

AsistenciaEE  *
>EE* +
(EE+ ,
entityEE, 2
=>EE3 5
{FF 
entityHH 
.HH 
ToTableHH 
(HH 
$strHH +
)HH+ ,
;HH, -
entityII 
.II 
HasKeyII 
(II 
aII 
=>II  "
aII# $
.II$ %
IdII% '
)II' (
;II( )
}LL 
)LL 
;LL 
}MM 	
}NN 
}OO ©6
òC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Infrastructure\Migrations\20260604004301_AddGimnasioIdToAdministrador.cs
	namespace 	

ControlFit
 
. 
Infrastructure #
.# $

Migrations$ .
{ 
public		 

partial		 
class		 (
AddGimnasioIdToAdministrador		 5
:		6 7
	Migration		8 A
{

 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
	AddColumn &
<& '
int' *
>* +
(+ ,
name 
: 
$str "
," #
table 
: 
$str "
," #
type 
: 
$str 
, 
nullable 
: 
false 
,  
defaultValue 
: 
$num 
)  
;  !
migrationBuilder 
. 
	AddColumn &
<& '
int' *
>* +
(+ ,
name 
: 
$str "
," #
table 
: 
$str &
,& '
type 
: 
$str 
, 
nullable 
: 
true 
) 
;  
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str "
," #
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
int& )
>) *
(* +
type+ /
:/ 0
$str1 6
,6 7
nullable8 @
:@ A
falseB G
)G H
.   

Annotation   #
(  # $
$str  $ 8
,  8 9
$str  : @
)  @ A
,  A B
	MiembroId!! 
=!! 
table!!  %
.!!% &
Column!!& ,
<!!, -
int!!- 0
>!!0 1
(!!1 2
type!!2 6
:!!6 7
$str!!8 =
,!!= >
nullable!!? G
:!!G H
false!!I N
)!!N O
,!!O P!
AsignacionMembresiaId"" )
=""* +
table"", 1
.""1 2
Column""2 8
<""8 9
int""9 <
>""< =
(""= >
type""> B
:""B C
$str""D I
,""I J
nullable""K S
:""S T
false""U Z
)""Z [
,""[ \
FechaHoraAcceso## #
=##$ %
table##& +
.##+ ,
Column##, 2
<##2 3
DateTime##3 ;
>##; <
(##< =
type##= A
:##A B
$str##C N
,##N O
nullable##P X
:##X Y
false##Z _
)##_ `
}$$ 
,$$ 
constraints%% 
:%% 
table%% "
=>%%# %
{&& 
table'' 
.'' 

PrimaryKey'' $
(''$ %
$str''% 4
,''4 5
x''6 7
=>''8 :
x''; <
.''< =
Id''= ?
)''? @
;''@ A
}(( 
)(( 
;(( 
migrationBuilder** 
.** 
CreateIndex** (
(**( )
name++ 
:++ 
$str++ /
,++/ 0
table,, 
:,, 
$str,, "
,,," #
column-- 
:-- 
$str-- $
)--$ %
;--% &
migrationBuilder// 
.// 
CreateIndex// (
(//( )
name00 
:00 
$str00 3
,003 4
table11 
:11 
$str11 &
,11& '
column22 
:22 
$str22 $
)22$ %
;22% &
migrationBuilder44 
.44 
AddForeignKey44 *
(44* +
name55 
:55 
$str55 <
,55< =
table66 
:66 
$str66 &
,66& '
column77 
:77 
$str77 $
,77$ %
principalTable88 
:88 
$str88  *
,88* +
principalColumn99 
:99  
$str99! %
)99% &
;99& '
migrationBuilder;; 
.;; 
AddForeignKey;; *
(;;* +
name<< 
:<< 
$str<< 8
,<<8 9
table== 
:== 
$str== "
,==" #
column>> 
:>> 
$str>> $
,>>$ %
principalTable?? 
:?? 
$str??  *
,??* +
principalColumn@@ 
:@@  
$str@@! %
,@@% &
onDeleteAA 
:AA 
ReferentialActionAA +
.AA+ ,
CascadeAA, 3
)AA3 4
;AA4 5
}BB 	
	protectedEE 
overrideEE 
voidEE 
DownEE  $
(EE$ %
MigrationBuilderEE% 5
migrationBuilderEE6 F
)EEF G
{FF 	
migrationBuilderGG 
.GG 
DropForeignKeyGG +
(GG+ ,
nameHH 
:HH 
$strHH <
,HH< =
tableII 
:II 
$strII &
)II& '
;II' (
migrationBuilderKK 
.KK 
DropForeignKeyKK +
(KK+ ,
nameLL 
:LL 
$strLL 8
,LL8 9
tableMM 
:MM 
$strMM "
)MM" #
;MM# $
migrationBuilderOO 
.OO 
	DropTableOO &
(OO& '
namePP 
:PP 
$strPP "
)PP" #
;PP# $
migrationBuilderRR 
.RR 
	DropIndexRR &
(RR& '
nameSS 
:SS 
$strSS /
,SS/ 0
tableTT 
:TT 
$strTT "
)TT" #
;TT# $
migrationBuilderVV 
.VV 
	DropIndexVV &
(VV& '
nameWW 
:WW 
$strWW 3
,WW3 4
tableXX 
:XX 
$strXX &
)XX& '
;XX' (
migrationBuilderZZ 
.ZZ 

DropColumnZZ '
(ZZ' (
name[[ 
:[[ 
$str[[ "
,[[" #
table\\ 
:\\ 
$str\\ "
)\\" #
;\\# $
migrationBuilder^^ 
.^^ 

DropColumn^^ '
(^^' (
name__ 
:__ 
$str__ "
,__" #
table`` 
:`` 
$str`` &
)``& '
;``' (
}aa 	
}bb 
}cc ¢O
èC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Infrastructure\Migrations\20260603160615_creacionAsignaiones.cs
	namespace 	

ControlFit
 
. 
Infrastructure #
.# $

Migrations$ .
{ 
public		 

partial		 
class		 
creacionAsignaiones		 ,
:		- .
	Migration		/ 8
{

 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
DropForeignKey +
(+ ,
name 
: 
$str 5
,5 6
table 
: 
$str  
)  !
;! "
migrationBuilder 
. 
DropPrimaryKey +
(+ ,
name 
: 
$str "
," #
table 
: 
$str  
)  !
;! "
migrationBuilder 
. 
RenameTable (
(( )
name 
: 
$str 
,  
newName 
: 
$str #
)# $
;$ %
migrationBuilder 
. 
AddPrimaryKey *
(* +
name 
: 
$str #
,# $
table 
: 
$str !
,! "
column 
: 
$str 
) 
; 
migrationBuilder 
. 
CreateTable (
(( )
name   
:   
$str   "
,  " #
columns!! 
:!! 
table!! 
=>!! !
new!!" %
{"" 
Id## 
=## 
table## 
.## 
Column## %
<##% &
int##& )
>##) *
(##* +
type##+ /
:##/ 0
$str##1 6
,##6 7
nullable##8 @
:##@ A
false##B G
)##G H
.$$ 

Annotation$$ #
($$# $
$str$$$ 8
,$$8 9
$str$$: @
)$$@ A
,$$A B
	MiembroId%% 
=%% 
table%%  %
.%%% &
Column%%& ,
<%%, -
int%%- 0
>%%0 1
(%%1 2
type%%2 6
:%%6 7
$str%%8 =
,%%= >
nullable%%? G
:%%G H
false%%I N
)%%N O
,%%O P
MembresiaId&& 
=&&  !
table&&" '
.&&' (
Column&&( .
<&&. /
int&&/ 2
>&&2 3
(&&3 4
type&&4 8
:&&8 9
$str&&: ?
,&&? @
nullable&&A I
:&&I J
false&&K P
)&&P Q
,&&Q R
FechaInicio'' 
=''  !
table''" '
.''' (
Column''( .
<''. /
DateTime''/ 7
>''7 8
(''8 9
type''9 =
:''= >
$str''? J
,''J K
nullable''L T
:''T U
false''V [
)''[ \
,''\ ]
FechaFin(( 
=(( 
table(( $
.(($ %
Column((% +
<((+ ,
DateTime((, 4
>((4 5
(((5 6
type((6 :
:((: ;
$str((< G
,((G H
nullable((I Q
:((Q R
false((S X
)((X Y
,((Y Z
Estado)) 
=)) 
table)) "
.))" #
Column))# )
<))) *
bool))* .
>)). /
())/ 0
type))0 4
:))4 5
$str))6 ;
,)); <
nullable))= E
:))E F
false))G L
)))L M
}** 
,** 
constraints++ 
:++ 
table++ "
=>++# %
{,, 
table-- 
.-- 

PrimaryKey-- $
(--$ %
$str--% 4
,--4 5
x--6 7
=>--8 :
x--; <
.--< =
Id--= ?
)--? @
;--@ A
}.. 
).. 
;.. 
migrationBuilder00 
.00 
CreateTable00 (
(00( )
name11 
:11 
$str11 !
,11! "
columns22 
:22 
table22 
=>22 !
new22" %
{33 
Id44 
=44 
table44 
.44 
Column44 %
<44% &
int44& )
>44) *
(44* +
type44+ /
:44/ 0
$str441 6
,446 7
nullable448 @
:44@ A
false44B G
)44G H
.55 

Annotation55 #
(55# $
$str55$ 8
,558 9
$str55: @
)55@ A
,55A B
Nombre66 
=66 
table66 "
.66" #
Column66# )
<66) *
string66* 0
>660 1
(661 2
type662 6
:666 7
$str668 G
,66G H
nullable66I Q
:66Q R
false66S X
)66X Y
,66Y Z
	Duraci√≥n77 
=77 
table77 $
.77$ %
Column77% +
<77+ ,
int77, /
>77/ 0
(770 1
type771 5
:775 6
$str777 <
,77< =
nullable77> F
:77F G
false77H M
)77M N
,77N O
Precio88 
=88 
table88 "
.88" #
Column88# )
<88) *
double88* 0
>880 1
(881 2
type882 6
:886 7
$str888 ?
,88? @
nullable88A I
:88I J
false88K P
)88P Q
,88Q R 
MaximoIngresosPorDia99 (
=99) *
table99+ 0
.990 1
Column991 7
<997 8
int998 ;
>99; <
(99< =
type99= A
:99A B
$str99C H
,99H I
nullable99J R
:99R S
true99T X
)99X Y
,99Y Z#
MaximoIngresosPorSemana:: +
=::, -
table::. 3
.::3 4
Column::4 :
<::: ;
int::; >
>::> ?
(::? @
type::@ D
:::D E
$str::F K
,::K L
nullable::M U
:::U V
true::W [
)::[ \
,::\ ]!
MaximoIngresosTotales;; )
=;;* +
table;;, 1
.;;1 2
Column;;2 8
<;;8 9
int;;9 <
>;;< =
(;;= >
type;;> B
:;;B C
$str;;D I
,;;I J
nullable;;K S
:;;S T
false;;U Z
);;Z [
,;;[ \
Estado<< 
=<< 
table<< "
.<<" #
Column<<# )
<<<) *
bool<<* .
><<. /
(<</ 0
type<<0 4
:<<4 5
$str<<6 ;
,<<; <
nullable<<= E
:<<E F
false<<G L
)<<L M
}== 
,== 
constraints>> 
:>> 
table>> "
=>>># %
{?? 
table@@ 
.@@ 

PrimaryKey@@ $
(@@$ %
$str@@% 3
,@@3 4
x@@5 6
=>@@7 9
x@@: ;
.@@; <
Id@@< >
)@@> ?
;@@? @
}AA 
)AA 
;AA 
migrationBuilderCC 
.CC 
AddForeignKeyCC *
(CC* +
nameDD 
:DD 
$strDD 6
,DD6 7
tableEE 
:EE 
$strEE  
,EE  !
columnFF 
:FF 
$strFF $
,FF$ %
principalTableGG 
:GG 
$strGG  *
,GG* +
principalColumnHH 
:HH  
$strHH! %
,HH% &
onDeleteII 
:II 
ReferentialActionII +
.II+ ,
CascadeII, 3
)II3 4
;II4 5
}JJ 	
	protectedMM 
overrideMM 
voidMM 
DownMM  $
(MM$ %
MigrationBuilderMM% 5
migrationBuilderMM6 F
)MMF G
{NN 	
migrationBuilderOO 
.OO 
DropForeignKeyOO +
(OO+ ,
namePP 
:PP 
$strPP 6
,PP6 7
tableQQ 
:QQ 
$strQQ  
)QQ  !
;QQ! "
migrationBuilderSS 
.SS 
	DropTableSS &
(SS& '
nameTT 
:TT 
$strTT "
)TT" #
;TT# $
migrationBuilderVV 
.VV 
	DropTableVV &
(VV& '
nameWW 
:WW 
$strWW !
)WW! "
;WW" #
migrationBuilderYY 
.YY 
DropPrimaryKeyYY +
(YY+ ,
nameZZ 
:ZZ 
$strZZ #
,ZZ# $
table[[ 
:[[ 
$str[[ !
)[[! "
;[[" #
migrationBuilder]] 
.]] 
RenameTable]] (
(]]( )
name^^ 
:^^ 
$str^^  
,^^  !
newName__ 
:__ 
$str__ "
)__" #
;__# $
migrationBuilderaa 
.aa 
AddPrimaryKeyaa *
(aa* +
namebb 
:bb 
$strbb "
,bb" #
tablecc 
:cc 
$strcc  
,cc  !
columndd 
:dd 
$strdd 
)dd 
;dd 
migrationBuilderff 
.ff 
AddForeignKeyff *
(ff* +
namegg 
:gg 
$strgg 5
,gg5 6
tablehh 
:hh 
$strhh  
,hh  !
columnii 
:ii 
$strii $
,ii$ %
principalTablejj 
:jj 
$strjj  )
,jj) *
principalColumnkk 
:kk  
$strkk! %
,kk% &
onDeletell 
:ll 
ReferentialActionll +
.ll+ ,
Cascadell, 3
)ll3 4
;ll4 5
}mm 	
}nn 
}oo ≠J
ÄC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Infrastructure\Migrations\20260515140921_Init.cs
	namespace 	

ControlFit
 
. 
Infrastructure #
.# $

Migrations$ .
{ 
public		 

partial		 
class		 
Init		 
:		 
	Migration		  )
{

 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str %
,% &
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
int& )
>) *
(* +
type+ /
:/ 0
$str1 6
,6 7
nullable8 @
:@ A
falseB G
)G H
. 

Annotation #
(# $
$str$ 8
,8 9
$str: @
)@ A
,A B
NombreCompleto "
=# $
table% *
.* +
Column+ 1
<1 2
string2 8
>8 9
(9 :
type: >
:> ?
$str@ O
,O P
	maxLengthQ Z
:Z [
$num\ _
,_ `
nullablea i
:i j
falsek p
)p q
,q r
Correo 
= 
table "
." #
Column# )
<) *
string* 0
>0 1
(1 2
type2 6
:6 7
$str8 G
,G H
	maxLengthI R
:R S
$numT W
,W X
nullableY a
:a b
falsec h
)h i
,i j

Contrasena 
=  
table! &
.& '
Column' -
<- .
string. 4
>4 5
(5 6
type6 :
:: ;
$str< K
,K L
	maxLengthM V
:V W
$numX [
,[ \
nullable] e
:e f
falseg l
)l m
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% 7
,7 8
x9 :
=>; =
x> ?
.? @
Id@ B
)B C
;C D
} 
) 
; 
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str 
,  
columns 
: 
table 
=> !
new" %
{   
Id!! 
=!! 
table!! 
.!! 
Column!! %
<!!% &
int!!& )
>!!) *
(!!* +
type!!+ /
:!!/ 0
$str!!1 6
,!!6 7
nullable!!8 @
:!!@ A
false!!B G
)!!G H
."" 

Annotation"" #
(""# $
$str""$ 8
,""8 9
$str"": @
)""@ A
,""A B
Nombre## 
=## 
table## "
.##" #
Column### )
<##) *
string##* 0
>##0 1
(##1 2
type##2 6
:##6 7
$str##8 G
,##G H
nullable##I Q
:##Q R
false##S X
)##X Y
,##Y Z
FechaCreacion$$ !
=$$" #
table$$$ )
.$$) *
Column$$* 0
<$$0 1
DateOnly$$1 9
>$$9 :
($$: ;
type$$; ?
:$$? @
$str$$A G
,$$G H
nullable$$I Q
:$$Q R
false$$S X
)$$X Y
,$$Y Z
	Direccion%% 
=%% 
table%%  %
.%%% &
Column%%& ,
<%%, -
string%%- 3
>%%3 4
(%%4 5
type%%5 9
:%%9 :
$str%%; J
,%%J K
nullable%%L T
:%%T U
false%%V [
)%%[ \
,%%\ ]
Estado&& 
=&& 
table&& "
.&&" #
Column&&# )
<&&) *
bool&&* .
>&&. /
(&&/ 0
type&&0 4
:&&4 5
$str&&6 ;
,&&; <
nullable&&= E
:&&E F
false&&G L
)&&L M
}'' 
,'' 
constraints(( 
:(( 
table(( "
=>((# %
{)) 
table** 
.** 

PrimaryKey** $
(**$ %
$str**% 1
,**1 2
x**3 4
=>**5 7
x**8 9
.**9 :
Id**: <
)**< =
;**= >
}++ 
)++ 
;++ 
migrationBuilder-- 
.-- 
CreateTable-- (
(--( )
name.. 
:.. 
$str.. 
,..  
columns// 
:// 
table// 
=>// !
new//" %
{00 
Id11 
=11 
table11 
.11 
Column11 %
<11% &
int11& )
>11) *
(11* +
type11+ /
:11/ 0
$str111 6
,116 7
nullable118 @
:11@ A
false11B G
)11G H
.22 

Annotation22 #
(22# $
$str22$ 8
,228 9
$str22: @
)22@ A
,22A B
Nombre33 
=33 
table33 "
.33" #
Column33# )
<33) *
string33* 0
>330 1
(331 2
type332 6
:336 7
$str338 G
,33G H
nullable33I Q
:33Q R
false33S X
)33X Y
,33Y Z
Correo44 
=44 
table44 "
.44" #
Column44# )
<44) *
string44* 0
>440 1
(441 2
type442 6
:446 7
$str448 G
,44G H
nullable44I Q
:44Q R
false44S X
)44X Y
,44Y Z
Telefono55 
=55 
table55 $
.55$ %
Column55% +
<55+ ,
string55, 2
>552 3
(553 4
type554 8
:558 9
$str55: I
,55I J
nullable55K S
:55S T
false55U Z
)55Z [
,55[ \
FechaNacimiento66 #
=66$ %
table66& +
.66+ ,
Column66, 2
<662 3
DateOnly663 ;
>66; <
(66< =
type66= A
:66A B
$str66C I
,66I J
nullable66K S
:66S T
false66U Z
)66Z [
,66[ \
Estado77 
=77 
table77 "
.77" #
Column77# )
<77) *
bool77* .
>77. /
(77/ 0
type770 4
:774 5
$str776 ;
,77; <
nullable77= E
:77E F
false77G L
)77L M
,77M N

GimnasioId88 
=88  
table88! &
.88& '
Column88' -
<88- .
int88. 1
>881 2
(882 3
type883 7
:887 8
$str889 >
,88> ?
nullable88@ H
:88H I
false88J O
)88O P
}99 
,99 
constraints:: 
::: 
table:: "
=>::# %
{;; 
table<< 
.<< 

PrimaryKey<< $
(<<$ %
$str<<% 1
,<<1 2
x<<3 4
=><<5 7
x<<8 9
.<<9 :
Id<<: <
)<<< =
;<<= >
table== 
.== 

ForeignKey== $
(==$ %
name>> 
:>> 
$str>> =
,>>= >
column?? 
:?? 
x??  !
=>??" $
x??% &
.??& '

GimnasioId??' 1
,??1 2
principalTable@@ &
:@@& '
$str@@( 1
,@@1 2
principalColumnAA '
:AA' (
$strAA) -
,AA- .
onDeleteBB  
:BB  !
ReferentialActionBB" 3
.BB3 4
CascadeBB4 ;
)BB; <
;BB< =
}CC 
)CC 
;CC 
migrationBuilderEE 
.EE 
CreateIndexEE (
(EE( )
nameFF 
:FF 
$strFF -
,FF- .
tableGG 
:GG 
$strGG  
,GG  !
columnHH 
:HH 
$strHH $
)HH$ %
;HH% &
}II 	
	protectedLL 
overrideLL 
voidLL 
DownLL  $
(LL$ %
MigrationBuilderLL% 5
migrationBuilderLL6 F
)LLF G
{MM 	
migrationBuilderNN 
.NN 
	DropTableNN &
(NN& '
nameOO 
:OO 
$strOO %
)OO% &
;OO& '
migrationBuilderQQ 
.QQ 
	DropTableQQ &
(QQ& '
nameRR 
:RR 
$strRR 
)RR  
;RR  !
migrationBuilderTT 
.TT 
	DropTableTT &
(TT& '
nameUU 
:UU 
$strUU 
)UU  
;UU  !
}VV 	
}WW 
}XX 