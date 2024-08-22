�
MC:\FIAP\GitAct\FiapChallengeGrupo32\Infrastructure\Repository\EFRepository.cs
	namespace 	
Infrastructure
 
. 

Repository #
{ 
public 

class 
EFRepository 
< 
T 
>  
:! "
IRepository# .
<. /
T/ 0
>0 1
where2 7
T8 9
:: ;
Contato< C
{ 
	protected		  
ApplicationDbContext		 &
_context		' /
;		/ 0
	protected

 
DbSet

 
<

 
T

 
>

 
_dbSet

 !
;

! "
public 
EFRepository 
(  
ApplicationDbContext 0
contexto1 9
)9 :
{
_context 
= 
contexto 
;  
_dbSet 
= 
_context 
. 
Set !
<! "
T" #
># $
($ %
)% &
;& '
} 	
public 
void 
Alterar 
( 
T 
entidade &
)& '
{ 	
_dbSet 
. 
Update 
( 
entidade "
)" #
;# $
_context 
. 
SaveChanges  
(  !
)! "
;" #
} 	
public 
void 
	Cadastrar 
( 
T 
entidade  (
)( )
{ 	
_dbSet 
. 
Add 
( 
entidade 
)  
;  !
_context 
. 
SaveChanges  
(  !
)! "
;" #
} 	
public 
void 
Deletar 
( 
int 
id  "
)" #
{ 	
_dbSet   
.   
Remove   
(   

ObterPorId   $
(  $ %
id  % '
)  ' (
)  ( )
;  ) *
_context!! 
.!! 
SaveChanges!!  
(!!  !
)!!! "
;!!" #
}"" 	
public$$ 
IList$$ 
<$$ 
T$$ 
>$$ 
ObterPorDDD$$ #
($$# $
int$$$ '
DDD$$( +
)$$+ ,
=>$$- /
_dbSet$$0 6
.%% 
Where%% 
(%% 
entity%% 
=>%%  
entity%%! '
.%%' (
DDD%%( +
==%%, .
DDD%%/ 2
)%%2 3
.%%3 4
ToList%%4 :
(%%: ;
)%%; <
;%%< =
public'' 
T'' 

ObterPorId'' 
('' 
int'' 
id''  "
)''" #
=>(( 
_dbSet(( 
.(( 
FirstOrDefault(( $
((($ %
entity((% +
=>((, .
entity((/ 5
.((5 6
Id((6 8
==((9 ;
id((< >
)((> ?
;((? @
public++ 
IList++ 
<++ 
T++ 
>++ 

ObterTodos++ "
(++" #
)++# $
=>,, 
_dbSet,, 
.,, 
ToList,, 
(,, 
),, 
;,, 
}-- 
}.. �
RC:\FIAP\GitAct\FiapChallengeGrupo32\Infrastructure\Repository\ContatoRepository.cs
	namespace 	
Infrastructure
 
. 

Repository #
{ 
public 

class 
ContatoRepository "
:# $
EFRepository% 1
<1 2
Contato2 9
>9 :
,: ;
IContatoRepository< N
{ 
public 
ContatoRepository  
(  ! 
ApplicationDbContext! 5
contexto6 >
)> ?
:@ A
baseB F
(F G
contextoG O
)O P
{		 	
}

 	
} 
} �
UC:\FIAP\GitAct\FiapChallengeGrupo32\Infrastructure\Repository\ApplicationDbContext.cs
	namespace 	
Infrastructure
 
. 

Repository #
{ 
public 

class  
ApplicationDbContext %
:& '
	DbContext( 1
{ 
private		 
readonly		 
string		 
_connectionString		  1
;		1 2
public  
ApplicationDbContext #
(# $
)$ %
{ 	
IConfiguration

=
new
ConfigurationBuilder
(
)
. 
SetBasePath 
( 
	AppDomain &
.& '

.4 5

)B C
. 
AddJsonFile 
( 
$str /
)/ 0
. 
Build 
( 
) 
; 
_connectionString 
= 

.- .
GetConnectionString. A
(A B
$strB T
)T U
;U V
} 	
public  
ApplicationDbContext #
(# $
string$ *
connectionString+ ;
); <
{ 	
_connectionString 
= 
connectionString  0
;0 1
} 	
public 
DbSet 
< 
Contato 
> 
Contato %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
	protected 
override 
void 

(- .#
DbContextOptionsBuilder. E
optionsBuilderF T
)T U
{ 	
if 
( 
! 
optionsBuilder 
.  
IsConfigured  ,
), -
{ 
optionsBuilder 
. 
UseSqlServer +
(+ ,
_connectionString, =
)= >
;> ?
}!! 
}"" 	
	protected## 
override## 
void## 
OnModelCreating##  /
(##/ 0
ModelBuilder##0 <
modelBuilder##= I
)##I J
{$$ 	
modelBuilder%% 
.%% +
ApplyConfigurationsFromAssembly%% 8
(%%8 9
typeof%%9 ?
(%%? @ 
ApplicationDbContext%%@ T
)%%T U
.%%U V
Assembly%%V ^
)%%^ _
;%%_ `
}&& 	
}'' 
}(( �
aC:\FIAP\GitAct\FiapChallengeGrupo32\Infrastructure\Migrations\20240804181520_PrimeiraMigration.cs
	namespace 	
Infrastructure
 
. 

Migrations #
{ 
public 

partial 
class 
PrimeiraMigration *
:+ ,
	Migration- 6
{		 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder
.
CreateTable
(
name 
: 
$str 
,  
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
int& )
>) *
(* +
type+ /
:/ 0
$str1 6
,6 7
nullable8 @
:@ A
falseB G
)G H
. 

Annotation #
(# $
$str$ 8
,8 9
$str: @
)@ A
,A B
Nome 
= 
table  
.  !
Column! '
<' (
string( .
>. /
(/ 0
type0 4
:4 5
$str6 D
,D E
nullableF N
:N O
falseP U
)U V
,V W
DDD 
= 
table 
.  
Column  &
<& '
string' -
>- .
(. /
type/ 3
:3 4
$str5 A
,A B
nullableC K
:K L
falseM R
)R S
,S T
Telefone 
= 
table $
.$ %
Column% +
<+ ,
string, 2
>2 3
(3 4
type4 8
:8 9
$str: F
,F G
nullableH P
:P Q
falseR W
)W X
,X Y
Email 
= 
table !
.! "
Column" (
<( )
string) /
>/ 0
(0 1
type1 5
:5 6
$str7 E
,E F
nullableG O
:O P
falseQ V
)V W
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
$str% 1
,1 2
x3 4
=>5 7
x8 9
.9 :
Id: <
)< =
;= >
} 
) 
; 
} 	
	protected 
override 
void 
Down  $
($ %
MigrationBuilder% 5
migrationBuilder6 F
)F G
{   	
migrationBuilder!! 
.!! 
	DropTable!! &
(!!& '
name"" 
:"" 
$str"" 
)""  
;""  !
}## 	
}$$ 
}%% �
XC:\FIAP\GitAct\FiapChallengeGrupo32\Infrastructure\Configuration\ContatoConfiguration.cs
	namespace 	
Infrastructure
 
. 

{ 
public 

class  
ContatoConfiguration %
:& '$
IEntityTypeConfiguration( @
<@ A
ContatoA H
>H I
{ 
public		 
void		 
	Configure		 
(		 
EntityTypeBuilder		 /
<		/ 0
Contato		0 7
>		7 8
builder		9 @
)		@ A
{

 	
builder 
. 
ToTable 
( 
$str %
)% &
;& '
builder 
. 
HasKey 
( 
p 
=> 
p  !
.! "
Id" $
)$ %
;% &
builder
.
Property
(
p
=>
p
.
Nome
)
.

(
$str
)
.

IsRequired
(
)
;
builder 
. 
Property 
( 
p 
=> !
p" #
.# $
DDD$ '
)' (
.( )

(6 7
$str7 C
)C D
.D E

IsRequiredE O
(O P
)P Q
;Q R
builder 
. 
Property 
( 
p 
=> !
p" #
.# $
Telefone$ ,
), -
.- .

(; <
$str< H
)H I
.I J

IsRequiredJ T
(T U
)U V
;V W
builder 
. 
Property 
( 
p 
=> !
p" #
.# $
Email$ )
)) *
.* +

(8 9
$str9 G
)G H
.H I

IsRequiredI S
(S T
)T U
;U V
} 	
} 
} 