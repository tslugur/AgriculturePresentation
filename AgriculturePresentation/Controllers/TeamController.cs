﻿using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriculturePresentation.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public IActionResult Index()
        {
            var values = _teamService.GetListAll();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddTeam()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddTeam(Team p)
        {
            TeamValidator validationRules = new TeamValidator();
            ValidationResult result = validationRules.Validate(p);
            if (result.IsValid)
            {
                _teamService.Insert(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
          
        }

        public IActionResult DeleteTeam(int id)
        {
            var values = _teamService.GetById(id);
            _teamService.Delete(values);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditTeam(int id)
        {
            var values = _teamService.GetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult EditTeam(Team p)
        {
            TeamValidator validationRules = new TeamValidator();
            ValidationResult result = validationRules.Validate(p);
            if (result.IsValid)
            {
                _teamService.Update(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();

        }
    }
}
