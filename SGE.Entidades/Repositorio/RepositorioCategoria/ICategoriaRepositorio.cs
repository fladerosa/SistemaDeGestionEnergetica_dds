using SGE.Entidades.Categorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Repositorio.RepositorioCategoria
{
    interface ICategoriaRepositorio
    {
        Categoria GetById(int CategoriaId);
    }
}
