using AutoMapper;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop;
using CarWorkshop.Application.CarWorkshop.Quaries.GetAllCarWorkshops;
using CarWorkshop.Application.CarWorkshop.Quaries.GetCarWorkshopByEncodedName;
using CarWorkshop.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarWorkshop.MVC.Controllers
{
    public class CarWorkshopController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        //private readonly ICarWorkshopService _carWorkshopService;

        //public CarWorkshopController(ICarWorkshopService carWorkshopService)
        //{
        //    _carWorkshopService = carWorkshopService;
        //}

        public CarWorkshopController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            //var carWorkshops = await _carWorkshopService.GetAll();

            var carWorkshops = await _mediator.Send(new GetAllCarWorkshopsQuery());
            return View(carWorkshops);
        }


        [Route("CarWorkshop/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));
            return View(dto);
        }

        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));

            EditCarWorkshopCommand model = _mapper.Map<EditCarWorkshopCommand>(dto);

            return View(dto);
        }

        [HttpPost]
        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName,EditCarWorkshopCommand command)
        {
            if (ModelState.IsValid)
            {
                return View(command);
            }

            // await _carWorkshopService.Create(carWorkshop);
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCarWorkshopCommand command)
        {
            if(ModelState.IsValid)
            {
                return View(command);
            }

            

            // await _carWorkshopService.Create(carWorkshop);
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));   
        }
    }
}
