using System;
using System.Collections.Generic;
namespace dataMgr{
    public interface IData<T>{
        public List<T> Leer();
        public void Guardar(List<T> lista);
    }
}