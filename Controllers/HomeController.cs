﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoAspNET.Models;
using TodoAspNET.Data;
using TodoAspNET.ViewModels;

namespace TodoAspNET.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tarefas = _context.Tarefas.ToList();
            
            var listaTarefas = new IndexViewModel(){
                Tarefas = tarefas
            };

            return View(listaTarefas);
        }
        
        [HttpPost]
        public IActionResult Index(IndexViewModel tarefa)
        {
            tarefa.TarefaToSend.Status = false;

            _context.Tarefas.Add(tarefa.TarefaToSend);
           
            _context.SaveChanges();
            
            var tarefas = _context.Tarefas.ToList();
            
            var listaTarefas = new IndexViewModel(){
                Tarefas = tarefas
            };

            return  View(listaTarefas);            

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
