create database SistemaEscolar;

use SistemaEscolar;

create table Estudiante (
id int not null auto_increment,
codigo varchar (20) not null,
nombre varchar (20) not null,
apellidoP varchar (20) not null,
apellidoM varchar (20) not null,
calle varchar (20) not null,
numero varchar (10) not null,
numeroInt varchar (10),
colonia varchar (20) not null,
telefono varchar (20) not null,
avisarA varchar (20) not null,
telEm varchar (20),
observaciones varchar (255),
usuario varchar (20) not null,
password varchar (20) not null,
primary key (id)
);

create table Maestro (
id int not null auto_increment,
codigo varchar (20) not null,
nombre varchar (20) not null,
apellidoP varchar (20) not null,
apellidoM varchar (20) not null,
calle varchar (20) not null,
numero varchar (10) not null,
numeroInt varchar (10),
colonia varchar (20) not null,
telefono varchar (20) not null,
celular varchar (20),
avisarA varchar (20) not null,
telEm varchar (20),
observaciones varchar (255),
usuario varchar (20) not null,
password varchar (20) not null,
primary key (id)
);

create table Materia (
id int not null auto_increment,
id_Estudiante int,
id_maestro int,
nombre varchar (20) not null,
codigo varchar (20) not null,
horas varchar (20) not null,
area varchar (20),
calificación varchar (20),
asistencias varchar (20),
promedio varchar (20) not null,
primary key (id),
foreign key (id_Estudiante) references Estudiante(id) on delete cascade,
foreign key (id_Maestro) references Maestro(id) on delete cascade
);

create table Grado (
id int not null auto_increment,
id_Estudiante int,
id_Maestro int,
numGrado varchar (10) not null,
descripcion varchar (30),
primary key (id),
foreign key (id_Estudiante) references Estudiante(id) on delete cascade,
foreign key (id_Maestro) references Maestro(id) on delete cascade
);

create table Cursos (
id int not null auto_increment,
id_Estudiante int,
id_Maestro int,
nombre varchar (20) not null,
codigo varchar (20) not null,
horas varchar (20) not null,
acreditado varchar (10),
noAcreditado varchar (10),
asistencias varchar (20),
primary key (id),
foreign key (id_Estudiante) references Estudiante (id) on delete cascade,
foreign key (id_Maestro) references Maestro (id) on delete cascade
);

create table Cordinacion (
id int not null auto_increment,
codigo varchar (20) not null,
nombre varchar (20) not null,
apellidoP varchar (20) not null,
apellidoM varchar (20) not null,
calle varchar (20) not null,
numero varchar (20) not null,
numeroInt varchar (20) not null,
colonia varchar (20) not null,
ciudad varchar (20),
telefono varchar (20) not null,
celular varchar (20),
avisarA varchar (20),
telEm varchar (20),
cargo varchar (20) not null,
observaciones varchar (255),
usuario varchar (20) not null,
password varchar (20) not null,
primary key (id)
);

create table Pagos (
id int not null auto_increment,
tipoPago varchar (30) not null,
id_Estudiante int,
id_cordinacion int,
importe varchar (30) not null,
codigo varchar (20) not null,
primary key (id),
foreign key (id_Estudiante) references Estudiante (id) on delete cascade,
foreign key (id_Cordinacion) references Cordinacion (id)on delete cascade
);

create table Nomina (
id int not null auto_increment,
id_Maestro int,
id_cordinacion int,
horasTrabajadas varchar (15) not null,
horasExtra varchar (15),
montoTotal varchar (20) not null,
observaciones varchar (30),
primary key (id),
foreign key (id_Maestro) references Maestro(id) on delete cascade,
foreign key (id_Cordinacion) references Cordinacion (id) on delete cascade
);


