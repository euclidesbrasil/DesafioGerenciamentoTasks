using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArquiteturaDesafio.Core.Domain.Enum
{
    public enum TaskStatus { None= 0, Pending = 1, Doing = 2, Done = 3 }
    public enum TaskAction { None =0, UpdateTask = 1, AddComment = 2, EditComment = 3}
    public enum TaskPriority { None = 0, Low = 1, Medium = 2, Hight = 3}

}
