﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Models
{
    /// <summary>
    /// Класс представляющий заполнитель для реального документа. Доступ к редактированию есть у Автора и Редактора.
    /// </summary>
    public class ProtectedDocument
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Editor { get; set; }
    }
}
