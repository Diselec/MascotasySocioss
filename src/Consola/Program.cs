// See https://aka.ms/new-console-template for more information
using dataMgr;
using App;
using Consola;


GestorClub gestor = new GestorClub();
Controlador controlador = new Controlador(gestor);
controlador.Run();

