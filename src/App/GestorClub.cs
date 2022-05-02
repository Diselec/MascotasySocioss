using Modelos;
using dataMgr;

namespace App{

    public class GestorClub
    {   
 
        private MascotasCSV repoMascotas = new MascotasCSV();
        private SociosCSV repoSocios = new SociosCSV();
 
        public List<Mascota> mascotas{get;}
        public List<Socio> socios{get;}

   

        public GestorClub(){
            mascotas = repoMascotas.Leer();
            socios = repoSocios.Leer();
            mascotas.ForEach(mascota =>{
                Socio owner = buscarSocio(mascota.dniSocio);
                mascota.dueno=owner;
            });
        }


        public void altaSocio(Socio socio){
            socios.Add(socio);
            repoSocios.Guardar(socios);
        }

        public void bajaSocio(Socio socio){
            socios.Remove(socio);
            repoSocios.Guardar(socios);
        }
        public void altaMascota(Mascota mascota){
            mascotas.Add(mascota);
            repoMascotas.Guardar(mascotas);
        }
        public void compraMascota(Mascota mascota, Socio socio){
            mascota.dniSocio = socio.dni;
            mascota.dueno = socio;
            repoMascotas.Guardar(mascotas);
        }
        public void bajaMascota(Mascota mascota){
            mascotas.Remove(mascota);
            repoMascotas.Guardar(mascotas);
        }

        public Socio buscarSocio(string dni) 
        => socios.Find(socio => socio.dni.Equals(dni));

        public List<Mascota> mascotasDeSocio(Socio socio) 
        => mascotas.FindAll(mascota => socio.dni.Equals(mascota.dniSocio));

        public List<Mascota> mascotasPorEspecie() 
            => mascotas.OrderByDescending(mascota => mascota.especie).ToList();
            
        
        public List<Mascota> mascotasPorEdad()
            => mascotas.OrderByDescending(mascota => mascota.Edad).ToList();
            
     
        

    }
}