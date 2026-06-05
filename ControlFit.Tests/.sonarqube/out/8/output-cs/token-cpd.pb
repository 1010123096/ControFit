…7
tC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Api\Controllers\MiembroController.cs
	namespace 	

ControlFit
 
. 
Api 
. 
Controllers $
{ 
[		 
ApiController		 
]		 
[

 
Route

 

(


 
$str

 
)

 
]

 
[ 
	Authorize 
] 
public 

class 
MiembroController "
:# $

Controller% /
{ 
private 
readonly 
MiembroService '
_miembro( 0
;0 1
public 
MiembroController  
(  !
MiembroService! /
miembro0 7
)7 8
{ 	
_miembro 
= 
miembro 
; 
} 	
[ 	
HttpPost	 
( 
$str 
) 
] 
public 
async 
Task 
< 
IActionResult '
>' (
Crear) .
(. /
[/ 0
FromBody0 8
]8 9

MiembroDTO: D
mdtoE I
)I J
{ 	
try 
{ 
var 
miembro 
= 
await #
_miembro$ ,
., -
Crearmiembro- 9
(9 :
mdto: >
)> ?
;? @
return 
Ok 
( 
new 
{ 
message 
= 
$str ;
,; <
data 
= 
mdto 
} 
) 
; 
} 
catch   
(   
	Exception   
ex   
)    
{!! 
return"" 

BadRequest"" !
(""! "
new""" %
{""& '
error""( -
="". /
ex""0 2
.""2 3
Message""3 :
}""; <
)""< =
;""= >
}## 
}$$ 	
[&& 	
HttpGet&&	 
(&& 
$str&& 
)&&  
]&&  !
public'' 
async'' 
Task'' 
<'' 
IActionResult'' '
>''' (
ObtenerTodos'') 5
(''5 6
)''6 7
{(( 	
try)) 
{** 
var++ 
miembros++ 
=++ 
await++ $
_miembro++% -
.++- .
ListarMiembro++. ;
(++; <
)++< =
;++= >
return,, 
Ok,, 
(,, 
new,, 
{-- 
message.. 
=.. 
$str.. ?
,..? @
data// 
=// 
miembros// #
,//# $
total00 
=00 
miembros00 $
.00$ %
Count00% *
}11 
)11 
;11 
}22 
catch33 
(33 
	Exception33 
ex33 
)33  
{44 
return55 

BadRequest55 !
(55! "
new55" %
{55& '
error55( -
=55. /
ex550 2
.552 3
Message553 :
}55; <
)55< =
;55= >
}66 
}77 	
[99 	
HttpGet99	 
(99 
$str99 
)99  
]99  !
public:: 
async:: 
Task:: 
<:: 
IActionResult:: '
>::' (
ObtenerPorId::) 5
(::5 6
int::6 9
id::: <
)::< =
{;; 	
try<< 
{== 
var>> 
miembro>> 
=>> 
await>> #
_miembro>>$ ,
.>>, -
ObtenerMiembroId>>- =
(>>= >
id>>> @
)>>@ A
;>>A B
return?? 
Ok?? 
(?? 
new?? 
{@@ 
messageAA 
=AA 
$strAA =
,AA= >
dataBB 
=BB 
miembroBB "
}CC 
)CC 
;CC 
}DD 
catchEE 
(EE 
	ExceptionEE 
exEE 
)EE  
{FF 
returnGG 

BadRequestGG !
(GG! "
newGG" %
{GG& '
errorGG( -
=GG. /
exGG0 2
.GG2 3
MessageGG3 :
}GG; <
)GG< =
;GG= >
}HH 
}II 	
[KK 	
HttpPutKK	 
(KK 
$strKK 
)KK 
]KK 
publicLL 
asyncLL 
TaskLL 
<LL 
IActionResultLL '
>LL' (

ActualizarLL) 3
(LL3 4
[LL4 5
FromBodyLL5 =
]LL= >

MiembroDTOLL? I

miembroDTOLLJ T
)LLT U
{MM 	
tryNN 
{OO 
varPP 
miembroPP 
=PP 
awaitPP #
_miembroPP$ ,
.PP, -
ActualizarMiembroPP- >
(PP> ?

miembroDTOPP? I
.PPI J
IdPPJ L
,PPL M

miembroDTOPPN X
.PPX Y
NombrePPY _
,PP_ `

miembroDTOPPa k
.PPk l
CorreoPPl r
,PPr s

miembroDTOPPt ~
.PP~ 
Telefono	PP á
)
PPá à
;
PPà â
returnQQ 
OkQQ 
(QQ 
newQQ 
{RR 
messageSS 
=SS 
$strSS @
,SS@ A
dataTT 
=TT 
miembroTT "
}UU 
)UU 
;UU 
}VV 
catchWW 
(WW 
	ExceptionWW 
exWW 
)WW  
{XX 
returnYY 

BadRequestYY !
(YY! "
newYY" %
{YY& '
errorYY( -
=YY. /
exYY0 2
.YY2 3
MessageYY3 :
}YY; <
)YY< =
;YY= >
}ZZ 
}[[ 	
[]] 	

HttpDelete]]	 
(]] 
$str]] 
)]] 
]]]  
public^^ 
async^^ 
Task^^ 
<^^ 
IActionResult^^ '
>^^' (
Eliminar^^) 1
(^^1 2
int^^2 5
id^^6 8
)^^8 9
{__ 	
try`` 
{aa 
awaitbb 
_miembrobb 
.bb 
EliminarMiembrobb .
(bb. /
idbb/ 1
)bb1 2
;bb2 3
returncc 
Okcc 
(cc 
newcc 
{cc 
messagecc  '
=cc( )
$strcc* J
}ccK L
)ccL M
;ccM N
}dd 
catchee 
(ee 
	Exceptionee 
exee 
)ee  
{ff 
returngg 

BadRequestgg !
(gg! "
newgg" %
{gg& '
errorgg( -
=gg. /
exgg0 2
.gg2 3
Messagegg3 :
}gg; <
)gg< =
;gg= >
}hh 
}ii 	
}jj 
}kk ôA
^C:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Api\Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
builder 
. 
Services 
. 
AddCors 
( 
options  
=>! #
{ 
options 
. 
	AddPolicy 
( 
$str %
,% &
policy 
=> 
{ 	
policy 
. 
WithOrigins 
( 
$str 6
)6 7
. 
AllowAnyHeader !
(! "
)" #
. 
AllowAnyMethod !
(! "
)" #
;# $
} 	
)	 

;
 
} 
) 
; 
builder 
. 
Services 
. 
AddAuthentication "
(" #
JwtBearerDefaults# 4
.4 5 
AuthenticationScheme5 I
)I J
.   
AddJwtBearer   
(   
options   
=>   
{!! 
options"" 
."" %
TokenValidationParameters"" )
=""* +
new"", /%
TokenValidationParameters""0 I
{## 	
ValidateIssuer$$ 
=$$ 
true$$ !
,$$! "
ValidateAudience%% 
=%% 
true%% #
,%%# $
ValidateLifetime&& 
=&& 
true&& #
,&&# $$
ValidateIssuerSigningKey'' $
=''% &
true''' +
,''+ ,
ValidIssuer)) 
=)) 
builder)) !
.))! "
Configuration))" /
[))/ 0
$str))0 <
]))< =
,))= >
ValidAudience** 
=** 
builder** #
.**# $
Configuration**$ 1
[**1 2
$str**2 @
]**@ A
,**A B
IssuerSigningKey,, 
=,, 
new,, " 
SymmetricSecurityKey,,# 7
(,,7 8
Encoding-- 
.-- 
UTF8-- 
.-- 
GetBytes-- &
(--& '
builder--' .
.--. /
Configuration--/ <
[--< =
$str--= F
]--F G
)--G H
)--H I
}.. 	
;..	 

}// 
)// 
;// 
builder00 
.00 
Services00 
.00 
AddAuthorization00 !
(00! "
)00" #
;00# $
builder33 
.33 
Services33 
.33 "
AddHttpContextAccessor33 '
(33' (
)33( )
;33) *
builder44 
.44 
Services44 
.44 
	AddScoped44 
<44 
IUserContextService44 .
,44. /
UserContextService440 B
>44B C
(44C D
)44D E
;44E F
builder66 
.66 
Services66 
.66 
AddControllers66 
(66  
)66  !
;66! "
builder88 
.88 
Services88 
.88 #
AddEndpointsApiExplorer88 (
(88( )
)88) *
;88* +
builder99 
.99 
Services99 
.99 
AddSwaggerGen99 
(99 
)99  
;99  !
builder;; 
.;; 
Services;; 
.;; 
AddDbContext;; 
<;; 
AppDbContext;; *
>;;* +
(;;+ ,
options;;, 3
=>;;4 6
options<< 	
.<<	 

UseSqlServer<<
 
(<< 
builder<< 
.<< 
Configuration<< ,
.<<, -
GetConnectionString<<- @
(<<@ A
$str<<A T
)<<T U
)<<U V
)<<V W
;<<W X
builder== 
.== 
Services== 
.== 
	AddScoped== 
<== 
ITokenService== (
,==( )
TokenService==* 6
>==6 7
(==7 8
)==8 9
;==9 :
builder>> 
.>> 
Services>> 
.>> 
	AddScoped>> 
<>> $
IAdministradorRepository>> 3
,>>3 4#
AdministradorRepository>>5 L
>>>L M
(>>M N
)>>N O
;>>O P
builder?? 
.?? 
Services?? 
.?? 
	AddScoped?? 
<?? "
RegistrarAdministrador?? 1
>??1 2
(??2 3
)??3 4
;??4 5
builder@@ 
.@@ 
Services@@ 
.@@ 
	AddScoped@@ 
<@@ 
LoginAdministrador@@ -
>@@- .
(@@. /
)@@/ 0
;@@0 1
builderAA 
.AA 
ServicesAA 
.AA 
	AddScopedAA 
<AA 
IMiembroRepositoryAA -
,AA- .
MiembroRepositoryAA/ @
>AA@ A
(AAA B
)AAB C
;AAC D
builderBB 
.BB 
ServicesBB 
.BB 
	AddScopedBB 
<BB 
MiembroServiceBB )
>BB) *
(BB* +
)BB+ ,
;BB, -
builderCC 
.CC 
ServicesCC 
.CC 
	AddScopedCC 
<CC 
IGimnasioRepositoryCC .
,CC. /
GimnasioRepositoryCC0 B
>CCB C
(CCC D
)CCD E
;CCE F
builderDD 
.DD 
ServicesDD 
.DD 
	AddScopedDD 
<DD 
GimnasioServiceDD *
>DD* +
(DD+ ,
)DD, -
;DD- .
builderEE 
.EE 
ServicesEE 
.EE 
	AddScopedEE 
<EE  
IMembresiaRepositoryEE /
,EE/ 0
MembresiaRepositoryEE1 D
>EED E
(EEE F
)EEF G
;EEG H
builderFF 
.FF 
ServicesFF 
.FF 
	AddScopedFF 
<FF 
MembresiaServiceFF +
>FF+ ,
(FF, -
)FF- .
;FF. /
builderGG 
.GG 
ServicesGG 
.GG 
	AddScopedGG 
<GG *
IAsignacionMembresiaRepositoryGG 9
,GG9 :)
AsignacionMembresiaRepositoryGG; X
>GGX Y
(GGY Z
)GGZ [
;GG[ \
builderHH 
.HH 
ServicesHH 
.HH 
	AddScopedHH 
<HH &
AsignacionMembresiaServiceHH 5
>HH5 6
(HH6 7
)HH7 8
;HH8 9
builderII 
.II 
ServicesII 
.II 
	AddScopedII 
<II !
IAsistenciaRepositoryII 0
,II0 1 
AsistenciaRepositoryII2 F
>IIF G
(IIG H
)IIH I
;III J
builderJJ 
.JJ 
ServicesJJ 
.JJ 
	AddScopedJJ 
<JJ 
RegistrarIngresoJJ +
>JJ+ ,
(JJ, -
)JJ- .
;JJ. /
varLL 
appLL 
=LL 	
builderLL
 
.LL 
BuildLL 
(LL 
)LL 
;LL 
ifPP 
(PP 
appPP 
.PP 
EnvironmentPP 
.PP 
IsDevelopmentPP !
(PP! "
)PP" #
)PP# $
{QQ 
appRR 
.RR 

UseSwaggerRR 
(RR 
)RR 
;RR 
appSS 
.SS 
UseSwaggerUISS 
(SS 
)SS 
;SS 
}TT 
appVV 
.VV 
UseHttpsRedirectionVV 
(VV 
)VV 
;VV 
appXX 
.XX 
UseCorsXX 
(XX 
$strXX 
)XX 
;XX 
appZZ 
.ZZ 
UseAuthenticationZZ 
(ZZ 
)ZZ 
;ZZ 
app\\ 
.\\ 
UseAuthorization\\ 
(\\ 
)\\ 
;\\ 
app^^ 
.^^ 
MapControllers^^ 
(^^ 
)^^ 
;^^ 
app`` 
.`` 
Run`` 
(`` 
)`` 	
;``	 
ü#
qC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Api\Controllers\SeedController.cs
	namespace 	

ControlFit
 
. 
Api 
. 
Controllers $
{ 
[ 
ApiController 
] 
[		 
Route		 

(		
 
$str		 
)		 
]		 
public

 

class

 
SeedController

 
:

  !
ControllerBase

" 0
{ 
private 
readonly "
RegistrarAdministrador /

_registrar0 :
;: ;
private 
readonly 
GimnasioService (
_gimnasioService) 9
;9 :
public 
SeedController 
( "
RegistrarAdministrador 4
	registrar5 >
,> ?
GimnasioService@ O
gimnasioServiceP _
)_ `
{ 	

_registrar 
= 
	registrar "
;" #
_gimnasioService 
= 
gimnasioService .
;. /
} 	
[ 	
HttpPost	 
] 
public 
async 
Task 
< 
IActionResult '
>' (
Seed) -
(- .
). /
{ 	
try 
{ 
var 
gymDto 
= 
new  
GimnasioCrearDTO! 1
(1 2
$str2 ?
,? @
$strA Z
)Z [
{ 
Estado 
= 
true !
} 
; 
var 
gym 
= 
await 
_gimnasioService  0
.0 1
CrearGimnasio1 >
(> ?
gymDto? E
)E F
;F G
var   
superAdminDto   !
=  " #
new  $ '$
RegistroAdministradorDTO  ( @
{!! 
nombreCompleto"" "
=""# $
$str""% 2
,""2 3
correo## 
=## 
$str## 3
,##3 4

contrasena$$ 
=$$  
$str$$! )
,$$) *

GimnasioId%% 
=%%  
null%%! %
}&& 
;&& 
await'' 

_registrar''  
.''  !"
EjecutarAsyncRegistrar''! 7
(''7 8
superAdminDto''8 E
)''E F
;''F G
var)) 
gymAdminDto)) 
=))  !
new))" %$
RegistroAdministradorDTO))& >
{** 
nombreCompleto++ "
=++# $
$str++% 4
,++4 5
correo,, 
=,, 
$str,, .
,,,. /

contrasena-- 
=--  
$str--! )
,--) *

GimnasioId.. 
=..  
gym..! $
...$ %
Id..% '
}// 
;// 
await00 

_registrar00  
.00  !"
EjecutarAsyncRegistrar00! 7
(007 8
gymAdminDto008 C
)00C D
;00D E
return22 
Ok22 
(22 
new22 
{33 
message44 
=44 
$str44 D
,44D E

superAdmin55 
=55  
new55! $
{55% &
correo55' -
=55. /
$str550 F
,55F G

contrasena55H R
=55S T
$str55U ]
,55] ^
tipo55_ c
=55d e
$str55f s
,55s t
ruta55u y
=55z {
$str	55| î
}
55ï ñ
,
55ñ ó
gymAdmin66 
=66 
new66 "
{66# $
correo66% +
=66, -
$str66. ?
,66? @

contrasena66A K
=66L M
$str66N V
,66V W
tipo66X \
=66] ^
$str66_ o
,66o p
ruta66q u
=66v w
$str	66x é
,
66é è
gimnasio
66ê ò
=
66ô ö
$str
66õ ®
}
66© ™
}77 
)77 
;77 
}88 
catch99 
(99 
	Exception99 
ex99 
)99  
{:: 
return;; 

BadRequest;; !
(;;! "
new;;" %
{;;& '
error;;( -
=;;. /
ex;;0 2
.;;2 3
Message;;3 :
};;; <
);;< =
;;;= >
}<< 
}== 	
}>> 
}?? àY
vC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Api\Controllers\MembresiaController.cs
	namespace 	

ControlFit
 
. 
Api 
. 
Controllers $
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
[ 
	Authorize 
] 
public 

class 
MembresiaController $
:% &
ControllerBase' 5
{ 
private 
readonly 
MembresiaService )
_service* 2
;2 3
public 
MembresiaController "
(" #
MembresiaService# 3
service4 ;
); <
{ 	
_service 
= 
service 
; 
} 	
[ 	
HttpPost	 
] 
public   
async   
Task   
<   
ActionResult   &
<  & '
MembresiaDTO  ' 3
>  3 4
>  4 5
Crear  6 ;
(  ; <
[  < =
FromBody  = E
]  E F
CrearMembresiaDTO  G X
dto  Y \
)  \ ]
{!! 	
try"" 
{## 
var$$ 
	membresia$$ 
=$$ 
await$$  %
_service$$& .
.$$. /
Crear$$/ 4
($$4 5
dto$$5 8
)$$8 9
;$$9 :
return%% 
Ok%% 
(%% 
new%% 
{&& 
message'' 
='' 
$str'' =
,''= >
data(( 
=(( 
new(( 
MembresiaDTO(( +
(((+ ,
	membresia)) !
.))! "
Id))" $
,))$ %
	membresia** !
.**! "
Nombre**" (
,**( )
	membresia++ !
.++! "
	Duraci√≥n++" *
,++* +
	membresia,, !
.,,! "
Precio,," (
,,,( )
	membresia-- !
.--! "
Estado--" (
,--( )
	membresia.. !
...! " 
MaximoIngresosPorDia.." 6
??..7 9
$num..: ;
,..; <
	membresia// !
.//! "#
MaximoIngresosPorSemana//" 9
??//: <
$num//= >
,//> ?
	membresia00 !
.00! "!
MaximoIngresosTotales00" 7
,007 8
	membresia11 !
.11! "

GimnasioId11" ,
)22 
}33 
)33 
;33 
}44 
catch55 
(55 
	Exception55 
ex55 
)55  
{66 
return77 

BadRequest77 !
(77! "
new77" %
{77& '
error77( -
=77. /
ex770 2
.772 3
Message773 :
}77; <
)77< =
;77= >
}88 
}99 	
[@@ 	
HttpGet@@	 
]@@ 
publicAA 
asyncAA 
TaskAA 
<AA 
ActionResultAA &
>AA& '
ObtenerTodasAA( 4
(AA4 5
)AA5 6
{BB 	
tryCC 
{DD 
varEE 

membresiasEE 
=EE  
awaitEE! &
_serviceEE' /
.EE/ 0
ListarTodosEE0 ;
(EE; <
)EE< =
;EE= >
varFF 
dtosFF 
=FF 

membresiasFF %
.FF% &
SelectFF& ,
(FF, -
mFF- .
=>FF/ 1
newFF2 5
MembresiaDTOFF6 B
(FFB C
mGG 
.GG 
IdGG 
,GG 
mHH 
.HH 
NombreHH 
,HH 
mII 
.II 
	Duraci√≥nII 
,II 
mJJ 
.JJ 
PrecioJJ 
,JJ 
mKK 
.KK 
EstadoKK 
,KK 
mLL 
.LL  
MaximoIngresosPorDiaLL *
??LL+ -
$numLL. /
,LL/ 0
mMM 
.MM #
MaximoIngresosPorSemanaMM -
??MM. 0
$numMM1 2
,MM2 3
mNN 
.NN !
MaximoIngresosTotalesNN +
,NN+ ,
mOO 
.OO 

GimnasioIdOO  
)PP 
)PP 
.PP 
ToListPP 
(PP 
)PP 
;PP 
returnRR 
OkRR 
(RR 
newRR 
{SS 
messageTT 
=TT 
$strTT A
,TTA B
dataUU 
=UU 
dtosUU 
,UU  
totalVV 
=VV 
dtosVV  
.VV  !
CountVV! &
}WW 
)WW 
;WW 
}XX 
catchYY 
(YY 
	ExceptionYY 
exYY 
)YY  
{ZZ 
return[[ 

BadRequest[[ !
([[! "
new[[" %
{[[& '
error[[( -
=[[. /
ex[[0 2
.[[2 3
Message[[3 :
}[[; <
)[[< =
;[[= >
}\\ 
}]] 	
[cc 	
HttpGetcc	 
(cc 
$strcc 
)cc 
]cc 
publicdd 
asyncdd 
Taskdd 
<dd 
ActionResultdd &
<dd& '
MembresiaDTOdd' 3
>dd3 4
>dd4 5
ObtenerPorIddd6 B
(ddB C
intddC F
idddG I
)ddI J
{ee 	
tryff 
{gg 
varhh 
	membresiahh 
=hh 
awaithh  %
_servicehh& .
.hh. /
ObtenerPorIdhh/ ;
(hh; <
idhh< >
)hh> ?
;hh? @
returnii 
Okii 
(ii 
newii 
{jj 
messagekk 
=kk 
$strkk ?
,kk? @
datall 
=ll 
newll 
MembresiaDTOll +
(ll+ ,
	membresiamm !
.mm! "
Idmm" $
,mm$ %
	membresiann !
.nn! "
Nombrenn" (
,nn( )
	membresiaoo !
.oo! "
	Duraci√≥noo" *
,oo* +
	membresiapp !
.pp! "
Preciopp" (
,pp( )
	membresiaqq !
.qq! "
Estadoqq" (
,qq( )
	membresiarr !
.rr! " 
MaximoIngresosPorDiarr" 6
??rr7 9
$numrr: ;
,rr; <
	membresiass !
.ss! "#
MaximoIngresosPorSemanass" 9
??ss: <
$numss= >
,ss> ?
	membresiatt !
.tt! "!
MaximoIngresosTotalestt" 7
,tt7 8
	membresiauu !
.uu! "

GimnasioIduu" ,
)vv 
}ww 
)ww 
;ww 
}xx 
catchyy 
(yy 
	Exceptionyy 
exyy 
)yy  
{zz 
return{{ 
NotFound{{ 
({{  
new{{  #
{{{$ %
error{{& +
={{, -
ex{{. 0
.{{0 1
Message{{1 8
}{{9 :
){{: ;
;{{; <
}|| 
}}} 	
[
ÉÉ 	
HttpPut
ÉÉ	 
(
ÉÉ 
$str
ÉÉ 
)
ÉÉ 
]
ÉÉ 
public
ÑÑ 
async
ÑÑ 
Task
ÑÑ 
<
ÑÑ 
ActionResult
ÑÑ &
<
ÑÑ& '
MembresiaDTO
ÑÑ' 3
>
ÑÑ3 4
>
ÑÑ4 5

Actualizar
ÑÑ6 @
(
ÑÑ@ A
int
ÑÑA D
id
ÑÑE G
,
ÑÑG H
[
ÑÑI J
FromBody
ÑÑJ R
]
ÑÑR S$
ActualizarMembresiaDTO
ÑÑT j
dto
ÑÑk n
)
ÑÑn o
{
ÖÖ 	
try
ÜÜ 
{
áá 
dto
àà 
.
àà 
Id
àà 
=
àà 
id
àà 
;
àà 
var
ââ 
actualizado
ââ 
=
ââ  !
await
ââ" '
_service
ââ( 0
.
ââ0 1

Actualizar
ââ1 ;
(
ââ; <
dto
ââ< ?
)
ââ? @
;
ââ@ A
if
ãã 
(
ãã 
!
ãã 
actualizado
ãã  
)
ãã  !
return
åå 

BadRequest
åå %
(
åå% &
new
åå& )
{
åå* +
error
åå, 1
=
åå2 3
$str
åå4 X
}
ååY Z
)
ååZ [
;
åå[ \
var
éé 
	membresia
éé 
=
éé 
await
éé  %
_service
éé& .
.
éé. /
ObtenerPorId
éé/ ;
(
éé; <
id
éé< >
)
éé> ?
;
éé? @
return
èè 
Ok
èè 
(
èè 
new
èè 
{
êê 
message
ëë 
=
ëë 
$str
ëë B
,
ëëB C
data
íí 
=
íí 
new
íí 
MembresiaDTO
íí +
(
íí+ ,
	membresia
ìì !
.
ìì! "
Id
ìì" $
,
ìì$ %
	membresia
îî !
.
îî! "
Nombre
îî" (
,
îî( )
	membresia
ïï !
.
ïï! "
	Duraci√≥n
ïï" *
,
ïï* +
	membresia
ññ !
.
ññ! "
Precio
ññ" (
,
ññ( )
	membresia
óó !
.
óó! "
Estado
óó" (
,
óó( )
	membresia
òò !
.
òò! ""
MaximoIngresosPorDia
òò" 6
??
òò7 9
$num
òò: ;
,
òò; <
	membresia
ôô !
.
ôô! "%
MaximoIngresosPorSemana
ôô" 9
??
ôô: <
$num
ôô= >
,
ôô> ?
	membresia
öö !
.
öö! "#
MaximoIngresosTotales
öö" 7
,
öö7 8
	membresia
õõ !
.
õõ! "

GimnasioId
õõ" ,
)
úú 
}
ùù 
)
ùù 
;
ùù 
}
ûû 
catch
üü 
(
üü 
	Exception
üü 
ex
üü 
)
üü  
{
†† 
return
°° 

BadRequest
°° !
(
°°! "
new
°°" %
{
°°& '
error
°°( -
=
°°. /
ex
°°0 2
.
°°2 3
Message
°°3 :
}
°°; <
)
°°< =
;
°°= >
}
¢¢ 
}
££ 	
[
©© 	

HttpDelete
©©	 
(
©© 
$str
©© 
)
©© 
]
©© 
public
™™ 
async
™™ 
Task
™™ 
<
™™ 
ActionResult
™™ &
>
™™& '
Eliminar
™™( 0
(
™™0 1
int
™™1 4
id
™™5 7
)
™™7 8
{
´´ 	
try
¨¨ 
{
≠≠ 
await
ÆÆ 
_service
ÆÆ 
.
ÆÆ 
Eliminar
ÆÆ '
(
ÆÆ' (
id
ÆÆ( *
)
ÆÆ* +
;
ÆÆ+ ,
return
ØØ 
Ok
ØØ 
(
ØØ 
new
ØØ 
{
ØØ 
message
ØØ  '
=
ØØ( )
$str
ØØ* L
}
ØØM N
)
ØØN O
;
ØØO P
}
∞∞ 
catch
±± 
(
±± 
	Exception
±± 
ex
±± 
)
±±  
{
≤≤ 
return
≥≥ 

BadRequest
≥≥ !
(
≥≥! "
new
≥≥" %
{
≥≥& '
error
≥≥( -
=
≥≥. /
ex
≥≥0 2
.
≥≥2 3
Message
≥≥3 :
}
≥≥; <
)
≥≥< =
;
≥≥= >
}
¥¥ 
}
µµ 	
}
∂∂ 
}∑∑ °—
vC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Api\Controllers\HistorialController.cs
	namespace

 	

ControlFit


 
.

 
Api

 
.

 
Controllers

 $
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
[ 
	Authorize 
] 
public 

class 
HistorialController $
:% &
ControllerBase' 5
{ 
private 
readonly 
IMiembroRepository +
_miembroRepository, >
;> ?
private 
readonly  
IMembresiaRepository - 
_membresiaRepository. B
;B C
private 
readonly *
IAsignacionMembresiaRepository 7!
_asignacionRepository8 M
;M N
private 
readonly !
IAsistenciaRepository .!
_asistenciaRepository/ D
;D E
private 
readonly 
IUserContextService ,
_userContext- 9
;9 :
public 
HistorialController "
(" #
IMiembroRepository 
miembroRepository 0
,0 1 
IMembresiaRepository    
membresiaRepository  ! 4
,  4 5*
IAsignacionMembresiaRepository!! * 
asignacionRepository!!+ ?
,!!? @!
IAsistenciaRepository"" ! 
asistenciaRepository""" 6
,""6 7
IUserContextService## 
userContext##  +
)##+ ,
{$$ 	
_miembroRepository%% 
=%%  
miembroRepository%%! 2
;%%2 3 
_membresiaRepository&&  
=&&! "
membresiaRepository&&# 6
;&&6 7!
_asignacionRepository'' !
=''" # 
asignacionRepository''$ 8
;''8 9!
_asistenciaRepository(( !
=((" # 
asistenciaRepository(($ 8
;((8 9
_userContext)) 
=)) 
userContext)) &
;))& '
}** 	
[00 	
HttpGet00	 
(00 
$str00 
)00 
]00 
public11 
async11 
Task11 
<11 
ActionResult11 &
>11& '&
ObtenerHistorialMembresias11( B
(11B C
[11C D
	FromQuery11D M
]11M N
int11O R
	miembroId11S \
)11\ ]
{22 	
try33 
{44 
if55 
(55 
	miembroId55 
<=55  
$num55! "
)55" #
return66 

BadRequest66 %
(66% &
new66& )
{66* +
error66, 1
=662 3
$str664 L
}66M N
)66N O
;66O P
var88 

gimnasioId88 
=88  
_userContext88! -
.88- .
GetGimnasioId88. ;
(88; <
)88< =
;88= >
if;; 
(;; 
_userContext;;  
.;;  !
EsAdminGimnasio;;! 0
(;;0 1
);;1 2
);;2 3
{<< 
var== 
miembro== 
===  !
await==" '
_miembroRepository==( :
.==: ;)
ObtenerPorIdValidandoGimnasio==; X
(==X Y
	miembroId==Y b
,==b c

gimnasioId==d n
)==n o
;==o p
if>> 
(>> 
miembro>> 
==>>  "
null>># '
)>>' (
return?? 
Unauthorized?? +
(??+ ,
new??, /
{??0 1
error??2 7
=??8 9
$str??: r
}??s t
)??t u
;??u v
}@@ 
elseAA 
{BB 
varCC 
miembroCC 
=CC  !
awaitCC" '
_miembroRepositoryCC( :
.CC: ;
ObtenerPorIdAsyncCC; L
(CCL M
	miembroIdCCM V
)CCV W
;CCW X
ifDD 
(DD 
miembroDD 
==DD  "
nullDD# '
)DD' (
returnEE 
NotFoundEE '
(EE' (
newEE( +
{EE, -
errorEE. 3
=EE4 5
$strEE6 M
}EEN O
)EEO P
;EEP Q
}FF 
varII 
asignacionesII  
=II! "
awaitII# (!
_asignacionRepositoryII) >
.II> ?%
ObtenerPorMiembroGimnasioII? X
(IIX Y
	miembroIdIIY b
,IIb c

gimnasioIdIId n
)IIn o
;IIo p
varKK 
historialMembresiasKK '
=KK( )
newKK* -
ListKK. 2
<KK2 3
dynamicKK3 :
>KK: ;
(KK; <
)KK< =
;KK= >
foreachLL 
(LL 
varLL 

asignacionLL '
inLL( *
asignacionesLL+ 7
)LL7 8
{MM 
varNN 
	membresiaNN !
=NN" #
awaitNN$ ) 
_membresiaRepositoryNN* >
.NN> ?
ObtenerPorIdAsyncNN? P
(NNP Q

asignacionNNQ [
.NN[ \
MembresiaIdNN\ g
)NNg h
;NNh i
historialMembresiasOO '
.OO' (
AddOO( +
(OO+ ,
newOO, /
{PP 
asignacionIdQQ $
=QQ% &

asignacionQQ' 1
.QQ1 2
IdQQ2 4
,QQ4 5
	membresiaRR !
=RR" #
newRR$ '
{SS 
idTT 
=TT  
	membresiaTT! *
?TT* +
.TT+ ,
IdTT, .
,TT. /
nombreUU "
=UU# $
	membresiaUU% .
?UU. /
.UU/ 0
NombreUU0 6
,UU6 7
duracionVV $
=VV% &
	membresiaVV' 0
?VV0 1
.VV1 2
	Duraci√≥nVV2 :
,VV: ;
precioWW "
=WW# $
	membresiaWW% .
?WW. /
.WW/ 0
PrecioWW0 6
}XX 
,XX 
fechaAsignacionYY '
=YY( )

asignacionYY* 4
.YY4 5
FechaAsignacionYY5 D
,YYD E
fechaVencimientoZZ (
=ZZ) *

asignacionZZ+ 5
.ZZ5 6
FechaVencimientoZZ6 F
,ZZF G
activa[[ 
=[[  

asignacion[[! +
.[[+ ,
EstaVigente[[, 7
([[7 8
)[[8 9
}\\ 
)\\ 
;\\ 
}]] 
return__ 
Ok__ 
(__ 
new__ 
{`` 
messageaa 
=aa 
$straa M
,aaM N
	miembroIdbb 
=bb 
	miembroIdbb  )
,bb) *
datacc 
=cc 
historialMembresiascc .
,cc. /
totaldd 
=dd 
historialMembresiasdd /
.dd/ 0
Countdd0 5
}ee 
)ee 
;ee 
}ff 
catchgg 
(gg 
	Exceptiongg 
exgg 
)gg  
{hh 
returnii 

BadRequestii !
(ii! "
newii" %
{ii& '
errorii( -
=ii. /
exii0 2
.ii2 3
Messageii3 :
}ii; <
)ii< =
;ii= >
}jj 
}kk 	
[pp 	
HttpGetpp	 
(pp 
$strpp 
)pp  
]pp  !
publicqq 
asyncqq 
Taskqq 
<qq 
ActionResultqq &
>qq& '(
ObtenerHistorialAsignacionesqq( D
(qqD E
[qqE F
	FromQueryqqF O
]qqO P
intqqQ T
	miembroIdqqU ^
)qq^ _
{rr 	
tryss 
{tt 
ifuu 
(uu 
	miembroIduu 
<=uu  
$numuu! "
)uu" #
returnvv 

BadRequestvv %
(vv% &
newvv& )
{vv* +
errorvv, 1
=vv2 3
$strvv4 L
}vvM N
)vvN O
;vvO P
varxx 

gimnasioIdxx 
=xx  
_userContextxx! -
.xx- .
GetGimnasioIdxx. ;
(xx; <
)xx< =
;xx= >
if{{ 
({{ 
_userContext{{  
.{{  !
EsAdminGimnasio{{! 0
({{0 1
){{1 2
){{2 3
{|| 
var}} 
miembro}} 
=}}  !
await}}" '
_miembroRepository}}( :
.}}: ;)
ObtenerPorIdValidandoGimnasio}}; X
(}}X Y
	miembroId}}Y b
,}}b c

gimnasioId}}d n
)}}n o
;}}o p
if~~ 
(~~ 
miembro~~ 
==~~  "
null~~# '
)~~' (
return 
Unauthorized +
(+ ,
new, /
{0 1
error2 7
=8 9
$str: r
}s t
)t u
;u v
}
ÄÄ 
else
ÅÅ 
{
ÇÇ 
var
ÉÉ 
miembro
ÉÉ 
=
ÉÉ  !
await
ÉÉ" ' 
_miembroRepository
ÉÉ( :
.
ÉÉ: ;
ObtenerPorIdAsync
ÉÉ; L
(
ÉÉL M
	miembroId
ÉÉM V
)
ÉÉV W
;
ÉÉW X
if
ÑÑ 
(
ÑÑ 
miembro
ÑÑ 
==
ÑÑ  "
null
ÑÑ# '
)
ÑÑ' (
return
ÖÖ 
NotFound
ÖÖ '
(
ÖÖ' (
new
ÖÖ( +
{
ÖÖ, -
error
ÖÖ. 3
=
ÖÖ4 5
$str
ÖÖ6 M
}
ÖÖN O
)
ÖÖO P
;
ÖÖP Q
}
ÜÜ 
var
àà 
asignaciones
àà  
=
àà! "
await
àà# (#
_asignacionRepository
àà) >
.
àà> ?'
ObtenerPorMiembroGimnasio
àà? X
(
ààX Y
	miembroId
ààY b
,
ààb c

gimnasioId
ààd n
)
ààn o
;
àào p
var
ää #
historialAsignaciones
ää )
=
ää* +
asignaciones
ää, 8
.
ää8 9
Select
ää9 ?
(
ää? @
a
ää@ A
=>
ääB D
new
ääE H
{
ãã 
id
åå 
=
åå 
a
åå 
.
åå 
Id
åå 
,
åå 
	miembroId
çç 
=
çç 
a
çç  !
.
çç! "
	MiembroId
çç" +
,
çç+ ,
membresiaId
éé 
=
éé  !
a
éé" #
.
éé# $
MembresiaId
éé$ /
,
éé/ 0
fechaAsignacion
èè #
=
èè$ %
a
èè& '
.
èè' (
FechaAsignacion
èè( 7
,
èè7 8
fechaVencimiento
êê $
=
êê% &
a
êê' (
.
êê( )
FechaVencimiento
êê) 9
,
êê9 :
activa
ëë 
=
ëë 
a
ëë 
.
ëë 
EstaVigente
ëë *
(
ëë* +
)
ëë+ ,
,
ëë, -
diasRestantes
íí !
=
íí" #
(
íí$ %
a
íí% &
.
íí& '
FechaVencimiento
íí' 7
-
íí8 9
DateTime
íí: B
.
ííB C
Now
ííC F
)
ííF G
.
ííG H
Days
ííH L
}
ìì 
)
ìì 
.
ìì 
ToList
ìì 
(
ìì 
)
ìì 
;
ìì 
return
ïï 
Ok
ïï 
(
ïï 
new
ïï 
{
ññ 
message
óó 
=
óó 
$str
óó O
,
óóO P
	miembroId
òò 
=
òò 
	miembroId
òò  )
,
òò) *
data
ôô 
=
ôô #
historialAsignaciones
ôô 0
,
ôô0 1
total
öö 
=
öö #
historialAsignaciones
öö 1
.
öö1 2
Count
öö2 7
}
õõ 
)
õõ 
;
õõ 
}
úú 
catch
ùù 
(
ùù 
	Exception
ùù 
ex
ùù 
)
ùù  
{
ûû 
return
üü 

BadRequest
üü !
(
üü! "
new
üü" %
{
üü& '
error
üü( -
=
üü. /
ex
üü0 2
.
üü2 3
Message
üü3 :
}
üü; <
)
üü< =
;
üü= >
}
†† 
}
°° 	
[
¶¶ 	
HttpGet
¶¶	 
(
¶¶ 
$str
¶¶ 
)
¶¶ 
]
¶¶  
public
ßß 
async
ßß 
Task
ßß 
<
ßß 
ActionResult
ßß &
>
ßß& ')
ObtenerHistorialAsistencias
ßß( C
(
ßßC D
[
®® 
	FromQuery
®® 
]
®® 
int
®® 
	miembroId
®® %
,
®®% &
[
©© 
	FromQuery
©© 
]
©© 
DateTime
©©  
?
©©  !
fechaInicio
©©" -
=
©©. /
null
©©0 4
,
©©4 5
[
™™ 
	FromQuery
™™ 
]
™™ 
DateTime
™™  
?
™™  !
fechaFin
™™" *
=
™™+ ,
null
™™- 1
)
™™1 2
{
´´ 	
try
¨¨ 
{
≠≠ 
if
ÆÆ 
(
ÆÆ 
	miembroId
ÆÆ 
<=
ÆÆ  
$num
ÆÆ! "
)
ÆÆ" #
return
ØØ 

BadRequest
ØØ %
(
ØØ% &
new
ØØ& )
{
ØØ* +
error
ØØ, 1
=
ØØ2 3
$str
ØØ4 L
}
ØØM N
)
ØØN O
;
ØØO P
var
±± 

gimnasioId
±± 
=
±±  
_userContext
±±! -
.
±±- .
GetGimnasioId
±±. ;
(
±±; <
)
±±< =
;
±±= >
if
¥¥ 
(
¥¥ 
_userContext
¥¥  
.
¥¥  !
EsAdminGimnasio
¥¥! 0
(
¥¥0 1
)
¥¥1 2
)
¥¥2 3
{
µµ 
var
∂∂ 
miembro
∂∂ 
=
∂∂  !
await
∂∂" ' 
_miembroRepository
∂∂( :
.
∂∂: ;+
ObtenerPorIdValidandoGimnasio
∂∂; X
(
∂∂X Y
	miembroId
∂∂Y b
,
∂∂b c

gimnasioId
∂∂d n
)
∂∂n o
;
∂∂o p
if
∑∑ 
(
∑∑ 
miembro
∑∑ 
==
∑∑  "
null
∑∑# '
)
∑∑' (
return
∏∏ 
Unauthorized
∏∏ +
(
∏∏+ ,
new
∏∏, /
{
∏∏0 1
error
∏∏2 7
=
∏∏8 9
$str
∏∏: r
}
∏∏s t
)
∏∏t u
;
∏∏u v
}
ππ 
else
∫∫ 
{
ªª 
var
ºº 
miembro
ºº 
=
ºº  !
await
ºº" ' 
_miembroRepository
ºº( :
.
ºº: ;
ObtenerPorIdAsync
ºº; L
(
ººL M
	miembroId
ººM V
)
ººV W
;
ººW X
if
ΩΩ 
(
ΩΩ 
miembro
ΩΩ 
==
ΩΩ  "
null
ΩΩ# '
)
ΩΩ' (
return
ææ 
NotFound
ææ '
(
ææ' (
new
ææ( +
{
ææ, -
error
ææ. 3
=
ææ4 5
$str
ææ6 M
}
ææN O
)
ææO P
;
ææP Q
}
øø 
var
¬¬ 
inicio
¬¬ 
=
¬¬ 
fechaInicio
¬¬ (
??
¬¬) +
DateTime
¬¬, 4
.
¬¬4 5
Now
¬¬5 8
.
¬¬8 9
	AddMonths
¬¬9 B
(
¬¬B C
-
¬¬C D
$num
¬¬D E
)
¬¬E F
;
¬¬F G
var
√√ 
fin
√√ 
=
√√ 
(
√√ 
fechaFin
√√ #
??
√√$ &
DateTime
√√' /
.
√√/ 0
Now
√√0 3
)
√√3 4
.
√√4 5
AddDays
√√5 <
(
√√< =
$num
√√= >
)
√√> ?
;
√√? @
var
≈≈ 
asistencias
≈≈ 
=
≈≈  !
await
≈≈" '#
_asistenciaRepository
≈≈( =
.
≈≈= >&
ObtenerPorMiembroEnRango
≈≈> V
(
≈≈V W
	miembroId
∆∆ 
,
∆∆ 
inicio
«« 
,
«« 
fin
»» 
,
»» 

gimnasioId
…… 
)
   
;
   
var
ÃÃ "
historialAsistencias
ÃÃ (
=
ÃÃ) *
asistencias
ÃÃ+ 6
.
ÕÕ 
OrderByDescending
ÕÕ &
(
ÕÕ& '
a
ÕÕ' (
=>
ÕÕ) +
a
ÕÕ, -
.
ÕÕ- .
FechaHoraAcceso
ÕÕ. =
)
ÕÕ= >
.
ŒŒ 
Select
ŒŒ 
(
ŒŒ 
a
ŒŒ 
=>
ŒŒ  
new
ŒŒ! $
{
œœ 
id
–– 
=
–– 
a
–– 
.
–– 
Id
–– !
,
––! "
	miembroId
—— !
=
——" #
a
——$ %
.
——% &
	MiembroId
——& /
,
——/ 0#
asignacionMembresiaId
““ -
=
““. /
a
““0 1
.
““1 2#
AsignacionMembresiaId
““2 G
,
““G H
fechaHoraAcceso
”” '
=
””( )
a
””* +
.
””+ ,
FechaHoraAcceso
””, ;
,
””; <
hora
‘‘ 
=
‘‘ 
a
‘‘  
.
‘‘  !
FechaHoraAcceso
‘‘! 0
.
‘‘0 1
ToString
‘‘1 9
(
‘‘9 :
$str
‘‘: A
)
‘‘A B
,
‘‘B C
fecha
’’ 
=
’’ 
a
’’  !
.
’’! "
FechaHoraAcceso
’’" 1
.
’’1 2
ToString
’’2 :
(
’’: ;
$str
’’; G
)
’’G H
}
÷÷ 
)
÷÷ 
.
◊◊ 
ToList
◊◊ 
(
◊◊ 
)
◊◊ 
;
◊◊ 
var
⁄⁄ 
estadisticas
⁄⁄  
=
⁄⁄! "
new
⁄⁄# &
{
€€ 
totalIngresos
‹‹ !
=
‹‹" #"
historialAsistencias
‹‹$ 8
.
‹‹8 9
Count
‹‹9 >
,
‹‹> ?
ingresosPorMes
›› "
=
››# $"
historialAsistencias
››% 9
.
ﬁﬁ 
GroupBy
ﬁﬁ  
(
ﬁﬁ  !
a
ﬁﬁ! "
=>
ﬁﬁ# %
a
ﬁﬁ& '
.
ﬁﬁ' (
fecha
ﬁﬁ( -
.
ﬁﬁ- .
	Substring
ﬁﬁ. 7
(
ﬁﬁ7 8
$num
ﬁﬁ8 9
,
ﬁﬁ9 :
$num
ﬁﬁ; <
)
ﬁﬁ< =
)
ﬁﬁ= >
.
ﬂﬂ 
Select
ﬂﬂ 
(
ﬂﬂ  
g
ﬂﬂ  !
=>
ﬂﬂ" $
new
ﬂﬂ% (
{
ﬂﬂ) *
mes
ﬂﬂ+ .
=
ﬂﬂ/ 0
g
ﬂﬂ1 2
.
ﬂﬂ2 3
Key
ﬂﬂ3 6
,
ﬂﬂ6 7
cantidad
ﬂﬂ8 @
=
ﬂﬂA B
g
ﬂﬂC D
.
ﬂﬂD E
Count
ﬂﬂE J
(
ﬂﬂJ K
)
ﬂﬂK L
}
ﬂﬂM N
)
ﬂﬂN O
.
‡‡ 
ToList
‡‡ 
(
‡‡  
)
‡‡  !
,
‡‡! "
ultimoIngreso
·· !
=
··" #"
historialAsistencias
··$ 8
.
··8 9
FirstOrDefault
··9 G
(
··G H
)
··H I
?
··I J
.
··J K
fechaHoraAcceso
··K Z
}
‚‚ 
;
‚‚ 
return
‰‰ 
Ok
‰‰ 
(
‰‰ 
new
‰‰ 
{
ÂÂ 
message
ÊÊ 
=
ÊÊ 
$str
ÊÊ N
,
ÊÊN O
	miembroId
ÁÁ 
=
ÁÁ 
	miembroId
ÁÁ  )
,
ÁÁ) *
filtros
ËË 
=
ËË 
new
ËË !
{
ÈÈ 
fechaInicio
ÍÍ #
=
ÍÍ$ %
inicio
ÍÍ& ,
,
ÍÍ, -
fechaFin
ÎÎ  
=
ÎÎ! "
fin
ÎÎ# &
}
ÏÏ 
,
ÏÏ 
estadisticas
ÌÌ  
=
ÌÌ! "
estadisticas
ÌÌ# /
,
ÌÌ/ 0
data
ÓÓ 
=
ÓÓ "
historialAsistencias
ÓÓ /
,
ÓÓ/ 0
total
ÔÔ 
=
ÔÔ "
historialAsistencias
ÔÔ 0
.
ÔÔ0 1
Count
ÔÔ1 6
}
 
)
 
;
 
}
ÒÒ 
catch
ÚÚ 
(
ÚÚ 
	Exception
ÚÚ 
ex
ÚÚ 
)
ÚÚ  
{
ÛÛ 
return
ÙÙ 

BadRequest
ÙÙ !
(
ÙÙ! "
new
ÙÙ" %
{
ÙÙ& '
error
ÙÙ( -
=
ÙÙ. /
ex
ÙÙ0 2
.
ÙÙ2 3
Message
ÙÙ3 :
}
ÙÙ; <
)
ÙÙ< =
;
ÙÙ= >
}
ıı 
}
ˆˆ 	
[
˚˚ 	
HttpGet
˚˚	 
(
˚˚ 
$str
˚˚ &
)
˚˚& '
]
˚˚' (
public
¸¸ 
async
¸¸ 
Task
¸¸ 
<
¸¸ 
ActionResult
¸¸ &
>
¸¸& '#
ObtenerResumenMiembro
¸¸( =
(
¸¸= >
int
¸¸> A
	miembroId
¸¸B K
)
¸¸K L
{
˝˝ 	
try
˛˛ 
{
ˇˇ 
if
ÄÄ 
(
ÄÄ 
	miembroId
ÄÄ 
<=
ÄÄ  
$num
ÄÄ! "
)
ÄÄ" #
return
ÅÅ 

BadRequest
ÅÅ %
(
ÅÅ% &
new
ÅÅ& )
{
ÅÅ* +
error
ÅÅ, 1
=
ÅÅ2 3
$str
ÅÅ4 L
}
ÅÅM N
)
ÅÅN O
;
ÅÅO P
var
ÉÉ 

gimnasioId
ÉÉ 
=
ÉÉ  
_userContext
ÉÉ! -
.
ÉÉ- .
GetGimnasioId
ÉÉ. ;
(
ÉÉ; <
)
ÉÉ< =
;
ÉÉ= >
var
ÜÜ 
miembro
ÜÜ 
=
ÜÜ 
_userContext
ÜÜ *
.
ÜÜ* +
EsAdminGimnasio
ÜÜ+ :
(
ÜÜ: ;
)
ÜÜ; <
?
áá 
await
áá  
_miembroRepository
áá .
.
áá. /+
ObtenerPorIdValidandoGimnasio
áá/ L
(
ááL M
	miembroId
ááM V
,
ááV W

gimnasioId
ááX b
)
ááb c
:
àà 
await
àà  
_miembroRepository
àà .
.
àà. /
ObtenerPorIdAsync
àà/ @
(
àà@ A
	miembroId
ààA J
)
ààJ K
;
ààK L
if
ää 
(
ää 
miembro
ää 
==
ää 
null
ää #
)
ää# $
return
ãã 
NotFound
ãã #
(
ãã# $
new
ãã$ '
{
ãã( )
error
ãã* /
=
ãã0 1
$str
ãã2 I
}
ããJ K
)
ããK L
;
ããL M
var
éé 
asignacionActiva
éé $
=
éé% &
await
éé' ,#
_asignacionRepository
éé- B
.
ééB C 
ObtenerActivaAsync
ééC U
(
ééU V
	miembroId
ééV _
)
éé_ `
;
éé` a
var
èè 
membresiaActiva
èè #
=
èè$ %
asignacionActiva
èè& 6
!=
èè7 9
null
èè: >
?
êê 
await
êê "
_membresiaRepository
êê 0
.
êê0 1
ObtenerPorIdAsync
êê1 B
(
êêB C
asignacionActiva
êêC S
.
êêS T
MembresiaId
êêT _
)
êê_ `
:
ëë 
null
ëë 
;
ëë 
var
îî "
asistenciasRecientes
îî (
=
îî) *
await
îî+ 0#
_asistenciaRepository
îî1 F
.
îîF G&
ObtenerPorMiembroEnRango
îîG _
(
îî_ `
	miembroId
ïï 
,
ïï 
DateTime
ññ 
.
ññ 
Now
ññ  
.
ññ  !
AddDays
ññ! (
(
ññ( )
-
ññ) *
$num
ññ* +
)
ññ+ ,
,
ññ, -
DateTime
óó 
.
óó 
Now
óó  
.
óó  !
AddDays
óó! (
(
óó( )
$num
óó) *
)
óó* +
,
óó+ ,

gimnasioId
òò 
)
ôô 
;
ôô 
return
õõ 
Ok
õõ 
(
õõ 
new
õõ 
{
úú 
message
ùù 
=
ùù 
$str
ùù I
,
ùùI J
data
ûû 
=
ûû 
new
ûû 
{
üü 
miembro
†† 
=
††  !
new
††" %
{
°° 
id
¢¢ 
=
¢¢  
miembro
¢¢! (
.
¢¢( )
Id
¢¢) +
,
¢¢+ ,
nombre
££ "
=
££# $
miembro
££% ,
.
££, -
Nombre
££- 3
,
££3 4
correo
§§ "
=
§§# $
miembro
§§% ,
.
§§, -
Correo
§§- 3
,
§§3 4
telefono
•• $
=
••% &
miembro
••' .
.
••. /
Telefono
••/ 7
,
••7 8
fechaNacimiento
¶¶ +
=
¶¶, -
miembro
¶¶. 5
.
¶¶5 6
FechaNacimiento
¶¶6 E
,
¶¶E F
estado
ßß "
=
ßß# $
miembro
ßß% ,
.
ßß, -
Estado
ßß- 3
,
ßß3 4

gimnasioId
®® &
=
®®' (
miembro
®®) 0
.
®®0 1

GimnasioId
®®1 ;
}
©© 
,
©© 
membresiaActual
™™ '
=
™™( )
membresiaActiva
™™* 9
!=
™™: <
null
™™= A
?
™™B C
new
™™D G
{
´´ 
id
¨¨ 
=
¨¨  
membresiaActiva
¨¨! 0
.
¨¨0 1
Id
¨¨1 3
,
¨¨3 4
nombre
≠≠ "
=
≠≠# $
membresiaActiva
≠≠% 4
.
≠≠4 5
Nombre
≠≠5 ;
,
≠≠; <
duracion
ÆÆ $
=
ÆÆ% &
membresiaActiva
ÆÆ' 6
.
ÆÆ6 7
	Duraci√≥n
ÆÆ7 ?
,
ÆÆ? @
precio
ØØ "
=
ØØ# $
membresiaActiva
ØØ% 4
.
ØØ4 5
Precio
ØØ5 ;
,
ØØ; <
fechaVencimiento
∞∞ ,
=
∞∞- .
asignacionActiva
∞∞/ ?
?
∞∞? @
.
∞∞@ A
FechaVencimiento
∞∞A Q
,
∞∞Q R
diasRestantes
±± )
=
±±* +
asignacionActiva
±±, <
!=
±±= ?
null
±±@ D
?
≤≤  !
(
≤≤" #
asignacionActiva
≤≤# 3
.
≤≤3 4
FechaVencimiento
≤≤4 D
-
≤≤E F
DateTime
≤≤G O
.
≤≤O P
Now
≤≤P S
)
≤≤S T
.
≤≤T U
Days
≤≤U Y
:
≥≥  !
$num
≥≥" #
}
¥¥ 
:
¥¥ 
null
¥¥  
,
¥¥  !
estadisticas
µµ $
=
µµ% &
new
µµ' *
{
∂∂ 
ingresosSemanal
∑∑ +
=
∑∑, -"
asistenciasRecientes
∑∑. B
.
∑∑B C
Count
∑∑C H
,
∑∑H I
ultimoIngreso
∏∏ )
=
∏∏* +"
asistenciasRecientes
∏∏, @
.
∏∏@ A
OrderByDescending
∏∏A R
(
∏∏R S
a
∏∏S T
=>
∏∏U W
a
∏∏X Y
.
∏∏Y Z
FechaHoraAcceso
∏∏Z i
)
∏∏i j
.
∏∏j k
FirstOrDefault
∏∏k y
(
∏∏y z
)
∏∏z {
?
∏∏{ |
.
∏∏| }
FechaHoraAcceso∏∏} å
}
ππ 
}
∫∫ 
}
ªª 
)
ªª 
;
ªª 
}
ºº 
catch
ΩΩ 
(
ΩΩ 
	Exception
ΩΩ 
ex
ΩΩ 
)
ΩΩ  
{
ææ 
return
øø 

BadRequest
øø !
(
øø! "
new
øø" %
{
øø& '
error
øø( -
=
øø. /
ex
øø0 2
.
øø2 3
Message
øø3 :
}
øø; <
)
øø< =
;
øø= >
}
¿¿ 
}
¡¡ 	
}
¬¬ 
}√√ ã:
uC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Api\Controllers\GimnasioController.cs
	namespace 	

ControlFit
 
. 
Api 
. 
Controllers $
{ 
[		 
ApiController		 
]		 
[

 
Route

 

(


 
$str

 
)

 
]

 
public 

class 
GimnasioController #
:$ %

Controller& 0
{ 
private 
readonly 
GimnasioService (
_gimnasioService) 9
;9 :
public 
GimnasioController !
(! "
GimnasioService" 1
gimnasioService2 A
)A B
{ 	
_gimnasioService 
= 
gimnasioService .
;. /
} 	
[ 	
HttpPost	 
( 
$str 
) 
] 
public 
async 
Task 
< 
IActionResult '
>' (
Crear) .
(. /
[/ 0
FromBody0 8
]8 9
GimnasioCrearDTO: J
gimnasioDTOK V
)V W
{ 	
try 
{ 
var 
gimnasio 
= 
await $
_gimnasioService% 5
.5 6
CrearGimnasio6 C
(C D
gimnasioDTOD O
)O P
;P Q
return 
Ok 
( 
new 
{ 
message 
= 
$str <
,< =
data 
= 
gimnasio #
} 
) 
; 
}   
catch!! 
(!! 
	Exception!! 
ex!! 
)!!  
{"" 
return## 

BadRequest## !
(##! "
new##" %
{##& '
error##( -
=##. /
ex##0 2
.##2 3
Message##3 :
}##; <
)##< =
;##= >
}$$ 
}%% 	
['' 	
HttpGet''	 
('' 
$str'' 
)''  
]''  !
public(( 
async(( 
Task(( 
<(( 
IActionResult(( '
>((' (
ObtenerPorId(() 5
(((5 6
int((6 9
id((: <
)((< =
{)) 	
try** 
{++ 
var,, 
gimnasio,, 
=,, 
await,, $
_gimnasioService,,% 5
.,,5 6
BuscarPorId,,6 A
(,,A B
id,,B D
),,D E
;,,E F
return-- 
Ok-- 
(-- 
new-- 
{.. 
message// 
=// 
$str// >
,//> ?
data00 
=00 
gimnasio00 #
}11 
)11 
;11 
}22 
catch33 
(33 
	Exception33 
ex33 
)33  
{44 
return55 

BadRequest55 !
(55! "
new55" %
{55& '
error55( -
=55. /
ex550 2
.552 3
Message553 :
}55; <
)55< =
;55= >
}66 
}77 	
[99 	
HttpGet99	 
(99 
$str99 
)99  
]99  !
public:: 
async:: 
Task:: 
<:: 
IActionResult:: '
>::' (
ObtenerTodos::) 5
(::5 6
)::6 7
{;; 	
try<< 
{== 
var>> 
	gimnasios>> 
=>> 
await>>  %
_gimnasioService>>& 6
.>>6 7
BuscarTodos>>7 B
(>>B C
)>>C D
;>>D E
return?? 
Ok?? 
(?? 
new?? 
{@@ 
messageAA 
=AA 
$strAA @
,AA@ A
dataBB 
=BB 
	gimnasiosBB $
,BB$ %
totalCC 
=CC 
	gimnasiosCC %
.CC% &
CountCC& +
}DD 
)DD 
;DD 
}EE 
catchFF 
(FF 
	ExceptionFF 
exFF 
)FF  
{GG 
returnHH 

BadRequestHH !
(HH! "
newHH" %
{HH& '
errorHH( -
=HH. /
exHH0 2
.HH2 3
MessageHH3 :
}HH; <
)HH< =
;HH= >
}II 
}JJ 	
[LL 	
HttpPutLL	 
(LL 
$strLL 
)LL 
]LL 
publicMM 
asyncMM 
TaskMM 
<MM 
IActionResultMM '
>MM' (

ActualizarMM) 3
(MM3 4
[MM4 5
FromBodyMM5 =
]MM= >!
GimnasioActualizarDTOMM? T
gimnasioDTOMMU `
)MM` a
{NN 	
tryOO 
{PP 
varQQ 
	resultadoQQ 
=QQ 
awaitQQ  %
_gimnasioServiceQQ& 6
.QQ6 7
ActualizarGimnasioQQ7 I
(QQI J
gimnasioDTOQQJ U
)QQU V
;QQV W
ifRR 
(RR 
!RR 
	resultadoRR 
)RR 
returnSS 

BadRequestSS %
(SS% &
newSS& )
{SS* +
errorSS, 1
=SS2 3
$strSS4 W
}SSX Y
)SSY Z
;SSZ [
varUU 
gimnasioUU 
=UU 
awaitUU $
_gimnasioServiceUU% 5
.UU5 6
BuscarPorIdUU6 A
(UUA B
gimnasioDTOUUB M
.UUM N
IdUUN P
)UUP Q
;UUQ R
returnVV 
OkVV 
(VV 
newVV 
{WW 
messageXX 
=XX 
$strXX A
,XXA B
dataYY 
=YY 
gimnasioYY #
}ZZ 
)ZZ 
;ZZ 
}[[ 
catch\\ 
(\\ 
	Exception\\ 
ex\\ 
)\\  
{]] 
return^^ 

BadRequest^^ !
(^^! "
new^^" %
{^^& '
error^^( -
=^^. /
ex^^0 2
.^^2 3
Message^^3 :
}^^; <
)^^< =
;^^= >
}__ 
}`` 	
[bb 	

HttpDeletebb	 
(bb 
$strbb 
)bb 
]bb  
publiccc 
asynccc 
Taskcc 
<cc 
IActionResultcc '
>cc' (
Eliminarcc) 1
(cc1 2
intcc2 5
idcc6 8
)cc8 9
{dd 	
tryee 
{ff 
awaitgg 
_gimnasioServicegg &
.gg& '
EliminarGimnasiogg' 7
(gg7 8
idgg8 :
)gg: ;
;gg; <
returnhh 
Okhh 
(hh 
newhh 
{hh 
messagehh  '
=hh( )
$strhh* K
}hhL M
)hhM N
;hhN O
}ii 
catchjj 
(jj 
	Exceptionjj 
exjj 
)jj  
{kk 
returnll 

BadRequestll !
(ll! "
newll" %
{ll& '
errorll( -
=ll. /
exll0 2
.ll2 3
Messagell3 :
}ll; <
)ll< =
;ll= >
}mm 
}nn 	
}oo 
}pp ¶
zC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Api\Controllers\AutenticacionController.cs
	namespace 	

ControlFit
 
. 
Api 
. 
Controllers $
{ 
[		 
ApiController		 
]		 
[

 
Route

 

(


 
$str

 
)

 
]

 
public 

class #
AutenticacionController (
:) *

Controller+ 5
{ 
private 
readonly "
RegistrarAdministrador /

_registrar0 :
;: ;
private 
readonly 
LoginAdministrador +
_login, 2
;2 3
public #
AutenticacionController &
(& '"
RegistrarAdministrador' =
	registrar> G
,G H
LoginAdministradorI [
login\ a
)a b
{ 	

_registrar 
= 
	registrar "
;" #
_login 
= 
login 
; 
} 	
[ 	
HttpPost	 
( 
$str 
) 
] 
public 

async 
Task 
< 
IActionResult #
># $
	Registrar% .
(. /
[/ 0
FromBody0 8
]8 9$
RegistroAdministradorDTO: R
dtoS V
)V W
{ 	
await 

_registrar 
. "
EjecutarAsyncRegistrar 3
(3 4
dto4 7
)7 8
;8 9
return 
Ok 
( 
new 
{ 
mensaje #
=$ %
$str& @
}A B
)B C
;C D
} 	
[ 	
AllowAnonymous	 
] 
[ 	
HttpPost	 
( 
$str 
) 
] 
public 

async 
Task 
< 
IActionResult #
># $
Login% *
(* +
[+ ,
FromBody, 4
]4 5!
LoginAdministradorDTO6 K
dtoL O
)O P
{   	
var!! 
id!! 
=!! 
await!! 
_login!! !
.!!! "
EjecutarAsyncLogin!!" 4
(!!4 5
dto!!5 8
)!!8 9
;!!9 :
if"" 
("" 
id"" 
=="" 
null"" 
)"" 
return## 
Unauthorized## #
(### $
new##$ '
{##( )
mensaje##* 1
=##2 3
$str##4 L
}##M N
)##N O
;##O P
return%% 
Ok%% 
(%% 
new%% 
{%% 
mensaje%% #
=%%$ %
$str%%& 5
,%%5 6
token%%7 <
=%%= >
id%%? A
}%%B C
)%%C D
;%%D E
}&& 	
}'' 
})) ˘Ñ
wC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Api\Controllers\AsistenciaController.cs
	namespace 	

ControlFit
 
. 
Api 
. 
Controllers $
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
[ 
	Authorize 
] 
public 

class  
AsistenciaController %
:& '
ControllerBase( 6
{ 
private 
readonly 
RegistrarIngreso )
_registrarIngreso* ;
;; <
private 
readonly !
IAsistenciaRepository .!
_asistenciaRepository/ D
;D E
private 
readonly 
IMiembroRepository +
_miembroRepository, >
;> ?
private 
readonly 
IUserContextService ,
_userContext- 9
;9 :
public  
AsistenciaController #
(# $
RegistrarIngreso 
registrarIngreso -
,- .!
IAsistenciaRepository ! 
asistenciaRepository" 6
,6 7
IMiembroRepository 
miembroRepository 0
,0 1
IUserContextService   
userContext    +
)  + ,
{!! 	
_registrarIngreso"" 
="" 
registrarIngreso""  0
;""0 1!
_asistenciaRepository## !
=##" # 
asistenciaRepository##$ 8
;##8 9
_miembroRepository$$ 
=$$  
miembroRepository$$! 2
;$$2 3
_userContext%% 
=%% 
userContext%% &
;%%& '
}&& 	
[,, 	
HttpPost,,	 
(,, 
$str,, 
),, 
],, 
public-- 
async-- 
Task-- 
<-- 
ActionResult-- &
>--& '
RegistrarIngreso--( 8
(--8 9
[--9 :
FromBody--: B
]--B C
RegistroIngresoDTO--D V
dto--W Z
)--Z [
{.. 	
try// 
{00 
if11 
(11 
dto11 
==11 
null11 
||11  "
dto11# &
.11& '
	MiembroId11' 0
<=111 3
$num114 5
)115 6
return22 

BadRequest22 %
(22% &
new22& )
{22* +
error22, 1
=222 3
$str224 L
}22M N
)22N O
;22O P
await44 
_registrarIngreso44 '
.44' (
EjecutarAsync44( 5
(445 6
dto446 9
.449 :
	MiembroId44: C
)44C D
;44D E
return66 
Ok66 
(66 
new66 
{77 
message88 
=88 
$str88 ?
,88? @
data99 
=99 
new99 
{99  
	miembroId99! *
=99+ ,
dto99- 0
.990 1
	MiembroId991 :
,99: ;
	fechaHora99< E
=99F G
DateTime99H P
.99P Q
Now99Q T
}99U V
}:: 
):: 
;:: 
};; 
catch<< 
(<< 
	Exception<< 
ex<< 
)<<  
{== 
return>> 

BadRequest>> !
(>>! "
new>>" %
{>>& '
error>>( -
=>>. /
ex>>0 2
.>>2 3
Message>>3 :
}>>; <
)>>< =
;>>= >
}?? 
}@@ 	
[GG 	
HttpGetGG	 
]GG 
publicHH 
asyncHH 
TaskHH 
<HH 
ActionResultHH &
>HH& '
ObtenerAsistenciasHH( :
(HH: ;
)HH; <
{II 	
tryJJ 
{KK 
varLL 

gimnasioIdLL 
=LL  
_userContextLL! -
.LL- .
GetGimnasioIdLL. ;
(LL; <
)LL< =
;LL= >
ListMM 
<MM 
dynamicMM 
>MM 
asistenciasMM )
;MM) *
ifOO 
(OO 
_userContextOO  
.OO  !
EsSuperAdminOO! -
(OO- .
)OO. /
)OO/ 0
{PP 
asistenciasQQ 
=QQ  !
awaitQQ" '!
_asistenciaRepositoryQQ( =
.QQ= >
ObtenerTodosAsyncQQ> O
(QQO P
)QQP Q
.RR 
ContinueWithRR %
(RR% &
asyncRR& +
tRR, -
=>RR. 0
{SS 
varTT 
listaTT  %
=TT& '
awaitTT( -
tTT. /
;TT/ 0
varUU 
dtosUU  $
=UU% &
newUU' *
ListUU+ /
<UU/ 0
dynamicUU0 7
>UU7 8
(UU8 9
)UU9 :
;UU: ;
foreachVV #
(VV$ %
varVV% (
aVV) *
inVV+ -
listaVV. 3
)VV3 4
{WW 
varXX  #
miembroXX$ +
=XX, -
awaitXX. 3
_miembroRepositoryXX4 F
.XXF G
ObtenerPorIdAsyncXXG X
(XXX Y
aXXY Z
.XXZ [
	MiembroIdXX[ d
)XXd e
;XXe f
dtosYY  $
.YY$ %
AddYY% (
(YY( )
newYY) ,
{ZZ  !
id[[$ &
=[[' (
a[[) *
.[[* +
Id[[+ -
,[[- .
	miembroId\\$ -
=\\. /
a\\0 1
.\\1 2
	MiembroId\\2 ;
,\\; <
nombreMiembro]]$ 1
=]]2 3
miembro]]4 ;
?]]; <
.]]< =
Nombre]]= C
,]]C D!
asignacionMembresiaId^^$ 9
=^^: ;
a^^< =
.^^= >!
AsignacionMembresiaId^^> S
,^^S T
fechaHoraAcceso__$ 3
=__4 5
a__6 7
.__7 8
FechaHoraAcceso__8 G
}``  !
)``! "
;``" #
}aa 
returnbb "
dtosbb# '
;bb' (
}cc 
)cc 
.cc 
Resultcc !
;cc! "
}dd 
elseee 
{ff 
asistenciasgg 
=gg  !
awaitgg" '!
_asistenciaRepositorygg( =
.gg= >#
ObtenerTodosPorGimnasiogg> U
(ggU V

gimnasioIdggV `
)gg` a
.hh 
ContinueWithhh %
(hh% &
asynchh& +
thh, -
=>hh. 0
{ii 
varjj 
listajj  %
=jj& '
awaitjj( -
tjj. /
;jj/ 0
varkk 
dtoskk  $
=kk% &
newkk' *
Listkk+ /
<kk/ 0
dynamickk0 7
>kk7 8
(kk8 9
)kk9 :
;kk: ;
foreachll #
(ll$ %
varll% (
all) *
inll+ -
listall. 3
)ll3 4
{mm 
varnn  #
miembronn$ +
=nn, -
awaitnn. 3
_miembroRepositorynn4 F
.nnF G
ObtenerPorIdAsyncnnG X
(nnX Y
annY Z
.nnZ [
	MiembroIdnn[ d
)nnd e
;nne f
dtosoo  $
.oo$ %
Addoo% (
(oo( )
newoo) ,
{pp  !
idqq$ &
=qq' (
aqq) *
.qq* +
Idqq+ -
,qq- .
	miembroIdrr$ -
=rr. /
arr0 1
.rr1 2
	MiembroIdrr2 ;
,rr; <
nombreMiembross$ 1
=ss2 3
miembross4 ;
?ss; <
.ss< =
Nombress= C
,ssC D!
asignacionMembresiaIdtt$ 9
=tt: ;
att< =
.tt= >!
AsignacionMembresiaIdtt> S
,ttS T
fechaHoraAccesouu$ 3
=uu4 5
auu6 7
.uu7 8
FechaHoraAccesouu8 G
}vv  !
)vv! "
;vv" #
}ww 
returnxx "
dtosxx# '
;xx' (
}yy 
)yy 
.yy 
Resultyy !
;yy! "
}zz 
return|| 
Ok|| 
(|| 
new|| 
{}} 
message~~ 
=~~ 
$str~~ B
,~~B C
data 
= 
asistencias &
,& '
total
ÄÄ 
=
ÄÄ 
asistencias
ÄÄ '
.
ÄÄ' (
Count
ÄÄ( -
}
ÅÅ 
)
ÅÅ 
;
ÅÅ 
}
ÇÇ 
catch
ÉÉ 
(
ÉÉ 
	Exception
ÉÉ 
ex
ÉÉ 
)
ÉÉ  
{
ÑÑ 
return
ÖÖ 

BadRequest
ÖÖ !
(
ÖÖ! "
new
ÖÖ" %
{
ÖÖ& '
error
ÖÖ( -
=
ÖÖ. /
ex
ÖÖ0 2
.
ÖÖ2 3
Message
ÖÖ3 :
}
ÖÖ; <
)
ÖÖ< =
;
ÖÖ= >
}
ÜÜ 
}
áá 	
[
çç 	
HttpGet
çç	 
(
çç 
$str
çç 
)
çç 
]
çç 
public
éé 
async
éé 
Task
éé 
<
éé 
ActionResult
éé &
>
éé& ' 
FiltrarAsistencias
éé( :
(
éé: ;
[
èè 
	FromQuery
èè 
]
èè 
int
èè 
?
èè 
	miembroId
èè &
=
èè' (
null
èè) -
,
èè- .
[
êê 
	FromQuery
êê 
]
êê 
DateTime
êê  
?
êê  !
fechaInicio
êê" -
=
êê. /
null
êê0 4
,
êê4 5
[
ëë 
	FromQuery
ëë 
]
ëë 
DateTime
ëë  
?
ëë  !
fechaFin
ëë" *
=
ëë+ ,
null
ëë- 1
)
ëë1 2
{
íí 	
try
ìì 
{
îî 
var
ïï 

gimnasioId
ïï 
=
ïï  
_userContext
ïï! -
.
ïï- .
GetGimnasioId
ïï. ;
(
ïï; <
)
ïï< =
;
ïï= >
if
òò 
(
òò 
	miembroId
òò 
.
òò 
HasValue
òò &
&&
òò' )
_userContext
òò* 6
.
òò6 7
EsAdminGimnasio
òò7 F
(
òòF G
)
òòG H
)
òòH I
{
ôô 
var
öö 
miembro
öö 
=
öö  !
await
öö" ' 
_miembroRepository
öö( :
.
öö: ;+
ObtenerPorIdValidandoGimnasio
öö; X
(
ööX Y
	miembroId
ööY b
.
ööb c
Value
ööc h
,
ööh i

gimnasioId
ööj t
)
ööt u
;
ööu v
if
õõ 
(
õõ 
miembro
õõ 
==
õõ  "
null
õõ# '
)
õõ' (
return
úú 
Unauthorized
úú +
(
úú+ ,
new
úú, /
{
úú0 1
error
úú2 7
=
úú8 9
$str
úú: q
}
úúr s
)
úús t
;
úút u
}
ùù 
var
†† 
inicio
†† 
=
†† 
fechaInicio
†† (
??
††) +
DateTime
††, 4
.
††4 5
Now
††5 8
.
††8 9
AddDays
††9 @
(
††@ A
-
††A B
$num
††B D
)
††D E
;
††E F
var
°° 
fin
°° 
=
°° 
fechaFin
°° "
??
°°# %
DateTime
°°& .
.
°°. /
Now
°°/ 2
.
°°2 3
AddDays
°°3 :
(
°°: ;
$num
°°; <
)
°°< =
;
°°= >
List
££ 
<
££ 
dynamic
££ 
>
££ 
	resultado
££ '
=
££( )
new
££* -
List
££. 2
<
££2 3
dynamic
££3 :
>
££: ;
(
££; <
)
££< =
;
££= >
if
•• 
(
•• 
	miembroId
•• 
.
•• 
HasValue
•• &
)
••& '
{
¶¶ 
var
ßß 
asistencias
ßß #
=
ßß$ %
await
ßß& +#
_asistenciaRepository
ßß, A
.
ßßA B&
ObtenerPorMiembroEnRango
ßßB Z
(
ßßZ [
	miembroId
®® !
.
®®! "
Value
®®" '
,
®®' (
inicio
©© 
,
©© 
fin
™™ 
,
™™ 

gimnasioId
´´ "
)
¨¨ 
;
¨¨ 
var
ÆÆ 
miembro
ÆÆ 
=
ÆÆ  !
await
ÆÆ" ' 
_miembroRepository
ÆÆ( :
.
ÆÆ: ;
ObtenerPorIdAsync
ÆÆ; L
(
ÆÆL M
	miembroId
ÆÆM V
.
ÆÆV W
Value
ÆÆW \
)
ÆÆ\ ]
;
ÆÆ] ^
foreach
ØØ 
(
ØØ 
var
ØØ  
a
ØØ! "
in
ØØ# %
asistencias
ØØ& 1
)
ØØ1 2
{
∞∞ 
	resultado
±± !
.
±±! "
Add
±±" %
(
±±% &
new
±±& )
{
≤≤ 
id
≥≥ 
=
≥≥  
a
≥≥! "
.
≥≥" #
Id
≥≥# %
,
≥≥% &
	miembroId
¥¥ %
=
¥¥& '
a
¥¥( )
.
¥¥) *
	MiembroId
¥¥* 3
,
¥¥3 4
nombreMiembro
µµ )
=
µµ* +
miembro
µµ, 3
?
µµ3 4
.
µµ4 5
Nombre
µµ5 ;
,
µµ; <#
asignacionMembresiaId
∂∂ 1
=
∂∂2 3
a
∂∂4 5
.
∂∂5 6#
AsignacionMembresiaId
∂∂6 K
,
∂∂K L
fechaHoraAcceso
∑∑ +
=
∑∑, -
a
∑∑. /
.
∑∑/ 0
FechaHoraAcceso
∑∑0 ?
}
∏∏ 
)
∏∏ 
;
∏∏ 
}
ππ 
}
∫∫ 
else
ªª 
{
ºº 
var
ΩΩ 
todas
ΩΩ 
=
ΩΩ 
_userContext
ΩΩ  ,
.
ΩΩ, -
EsSuperAdmin
ΩΩ- 9
(
ΩΩ9 :
)
ΩΩ: ;
?
ææ 
await
ææ #
_asistenciaRepository
ææ  5
.
ææ5 6
ObtenerTodosAsync
ææ6 G
(
ææG H
)
ææH I
:
øø 
await
øø #
_asistenciaRepository
øø  5
.
øø5 6%
ObtenerTodosPorGimnasio
øø6 M
(
øøM N

gimnasioId
øøN X
)
øøX Y
;
øøY Z
var
¡¡ 
	filtradas
¡¡ !
=
¡¡" #
todas
¡¡$ )
.
¬¬ 
Where
¬¬ 
(
¬¬ 
a
¬¬  
=>
¬¬! #
a
¬¬$ %
.
¬¬% &
FechaHoraAcceso
¬¬& 5
>=
¬¬6 8
inicio
¬¬9 ?
&&
¬¬@ B
a
¬¬C D
.
¬¬D E
FechaHoraAcceso
¬¬E T
<=
¬¬U W
fin
¬¬X [
)
¬¬[ \
.
√√ 
ToList
√√ 
(
√√  
)
√√  !
;
√√! "
foreach
≈≈ 
(
≈≈ 
var
≈≈  
a
≈≈! "
in
≈≈# %
	filtradas
≈≈& /
)
≈≈/ 0
{
∆∆ 
var
«« 
miembro
«« #
=
««$ %
await
««& + 
_miembroRepository
««, >
.
««> ?
ObtenerPorIdAsync
««? P
(
««P Q
a
««Q R
.
««R S
	MiembroId
««S \
)
««\ ]
;
««] ^
	resultado
»» !
.
»»! "
Add
»»" %
(
»»% &
new
»»& )
{
…… 
id
   
=
    
a
  ! "
.
  " #
Id
  # %
,
  % &
	miembroId
ÀÀ %
=
ÀÀ& '
a
ÀÀ( )
.
ÀÀ) *
	MiembroId
ÀÀ* 3
,
ÀÀ3 4
nombreMiembro
ÃÃ )
=
ÃÃ* +
miembro
ÃÃ, 3
?
ÃÃ3 4
.
ÃÃ4 5
Nombre
ÃÃ5 ;
,
ÃÃ; <#
asignacionMembresiaId
ÕÕ 1
=
ÕÕ2 3
a
ÕÕ4 5
.
ÕÕ5 6#
AsignacionMembresiaId
ÕÕ6 K
,
ÕÕK L
fechaHoraAcceso
ŒŒ +
=
ŒŒ, -
a
ŒŒ. /
.
ŒŒ/ 0
FechaHoraAcceso
ŒŒ0 ?
}
œœ 
)
œœ 
;
œœ 
}
–– 
}
—— 
return
”” 
Ok
”” 
(
”” 
new
”” 
{
‘‘ 
message
’’ 
=
’’ 
$str
’’ B
,
’’B C
filtros
÷÷ 
=
÷÷ 
new
÷÷ !
{
◊◊ 
	miembroId
ÿÿ !
=
ÿÿ" #
	miembroId
ÿÿ$ -
,
ÿÿ- .
fechaInicio
ŸŸ #
=
ŸŸ$ %
inicio
ŸŸ& ,
,
ŸŸ, -
fechaFin
⁄⁄  
=
⁄⁄! "
fin
⁄⁄# &
}
€€ 
,
€€ 
data
‹‹ 
=
‹‹ 
	resultado
‹‹ $
,
‹‹$ %
total
›› 
=
›› 
	resultado
›› %
.
››% &
Count
››& +
}
ﬁﬁ 
)
ﬁﬁ 
;
ﬁﬁ 
}
ﬂﬂ 
catch
‡‡ 
(
‡‡ 
	Exception
‡‡ 
ex
‡‡ 
)
‡‡  
{
·· 
return
‚‚ 

BadRequest
‚‚ !
(
‚‚! "
new
‚‚" %
{
‚‚& '
error
‚‚( -
=
‚‚. /
ex
‚‚0 2
.
‚‚2 3
Message
‚‚3 :
}
‚‚; <
)
‚‚< =
;
‚‚= >
}
„„ 
}
‰‰ 	
}
ÂÂ 
}ÊÊ ì
ÄC:\Users\alber\OneDrive\Escritorio\Proyectos pagos\GYMSOF\ControlFit\ControlFit.Api\Controllers\AsignacionMembresiaController.cs
	namespace 	

ControlFit
 
. 
Api 
. 
Controllers $
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
public		 

class		 )
AsignacionMembresiaController		 .
:		/ 0

Controller		1 ;
{

 
private 
readonly &
AsignacionMembresiaService 3
_service4 <
;< =
public )
AsignacionMembresiaController ,
(, -&
AsignacionMembresiaService &
service' .
). /
{ 	
_service 
= 
service 
; 
} 	
[ 	
HttpPost	 
] 
public 
async 
Task 
< 
IActionResult '
>' (
Crear) .
(. /'
AsignacionMembresiaCrearDTO '
dto( +
)+ ,
{ 	
var 
	resultado 
= 
await !
_service" *
.* +
Crear+ 0
(0 1
dto1 4
)4 5
;5 6
return 
Ok 
( 
	resultado 
)  
;  !
} 	
[ 	
HttpGet	 
] 
public 
async 
Task 
< 
IActionResult '
>' (
ObtenerTodos) 5
(5 6
)6 7
{ 	
return 
Ok 
( 
await 
_service $
.$ %
ObtenerTodos% 1
(1 2
)2 3
)3 4
;4 5
}   	
["" 	
HttpGet""	 
("" 
$str"" 
)"" 
]"" 
public## 
async## 
Task## 
<## 
IActionResult## '
>##' (
ObtenerPorId##) 5
(##5 6
int##6 9
id##: <
)##< =
{$$ 	
return%% 
Ok%% 
(%% 
await%% 
_service%% $
.%%$ %
ObtenerPorId%%% 1
(%%1 2
id%%2 4
)%%4 5
)%%5 6
;%%6 7
}&& 	
[(( 	

HttpDelete((	 
((( 
$str(( 
)(( 
](( 
public)) 
async)) 
Task)) 
<)) 
IActionResult)) '
>))' (
Eliminar))) 1
())1 2
int))2 5
id))6 8
)))8 9
{** 	
await++ 
_service++ 
.++ 
Eliminar++ #
(++# $
id++$ &
)++& '
;++' (
return-- 
Ok-- 
(-- 
)-- 
;-- 
}.. 	
}// 
}00 