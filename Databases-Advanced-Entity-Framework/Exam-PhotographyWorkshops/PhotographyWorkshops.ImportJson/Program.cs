using System;
using PhotographyWorkshops.Data;
using AutoMapper;
using PhotographyWorkshops.Dtos;
using PhotographyWorkshops.Models;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Data.Entity.Validation;
using System.Reflection;
using System.Linq;

namespace PhotographyWorkshops.ImportJson
{
    class Program
    {
        private const string LENSES_PATH = "../../../datasets/lenses.json";
        private const string CAMERAS_PATH = "../../../datasets/cameras.json";
        private const string PHOTOGRAPHERS_PATH = "../../../datasets/photographers.json";

        private const string ERROR_MESSAGE = "Error. Invalid data provided";

        static void Main()
        {
            var context = new PhotographyWorkshopsContext();
            context.Database.Initialize(true);

            UnitOfWork unit = new UnitOfWork();
            ConfigureMapping(unit);

            //ImportDataFromJson(unit);
        }

        private static void ImportDataFromJson(UnitOfWork unit)
        {
            ImportLenses(unit);
            ImportCameras(unit);
            ImportPhotographers(unit);
        }

        private static void ImportPhotographers(UnitOfWork unit)
        {
            string json = File.ReadAllText(PHOTOGRAPHERS_PATH);
            IEnumerable<PhotographerDto> photographersDtos = JsonConvert
                .DeserializeObject<IEnumerable<PhotographerDto>>(json);
            foreach (var photographerDto in photographersDtos)
            {
                if (photographerDto.FirstName == null || photographerDto.LastName == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                Photographer photographer = new Photographer()
                {
                    FirstName = photographerDto.FirstName,
                    LastName = photographerDto.LastName,
                    Phone = photographerDto.Phone
                };

                photographer.PrimaryCamera = GetRandomCamera(unit);
                photographer.SecondaryCamera = GetRandomCamera(unit);
                foreach (int lensId in photographerDto.Lenses)
                {
                    Lens lens = unit.Lenses.First(l => l.Id == lensId);
                    if (lens == null || !IsCompatible(lens, photographer) )
                    {
                        continue;
                    }

                    photographer.Lenses.Add(lens);
                }

                try
                { 
                    unit.Photographers.Add(photographer);
                    unit.Commit();

                    Console.WriteLine($"Successfully imported {photographerDto.FirstName} {photographerDto.LastName} | Lenses: {photographerDto.Lenses.Count}");
                }
                catch (DbEntityValidationException)
                {
                    unit.Photographers.Remove(photographer);
                    Console.WriteLine(ERROR_MESSAGE);
                }
            }
        }

        private static bool IsCompatible(Lens lens, Photographer photographer)
        {
            if (lens.CompatibleWith == photographer.PrimaryCamera.Make ||
                lens.CompatibleWith == photographer.SecondaryCamera.Make)
            {
                return true;
            }

            return false;
        }

        private static Camera GetRandomCamera(UnitOfWork unit)
        {
            Random rnd = new Random();
            int randomId = rnd.Next(1, unit.Cameras.GetAll().Count());
            var camera = unit.Cameras.Find(randomId);
            return camera;
        }

        private static void ImportCameras(UnitOfWork unit)
        {
            string json = File.ReadAllText(CAMERAS_PATH);
            IEnumerable<CameraDto> camerasDtos = JsonConvert
                .DeserializeObject<IEnumerable<CameraDto>>(json);
            foreach (var cameraDto in camerasDtos)
            {
                if (cameraDto.Type == null || cameraDto.Make == null ||
                    cameraDto.Model == null || cameraDto.MinISO < 0)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                Camera camera = MapCameraByType(cameraDto);
                
                try
                {
                    unit.Cameras.Add(camera);
                    unit.Commit();
                    Console.WriteLine($"Successfully imported {cameraDto.Type} {cameraDto.Make} {cameraDto.Model}");
                }
                catch (DbEntityValidationException)
                {
                    unit.Cameras.Remove(camera);
                    Console.WriteLine(ERROR_MESSAGE);
                }
            }
        }

        private static Camera MapCameraByType(CameraDto cameraDto)
        {
            var cameraType = Assembly.GetAssembly(typeof(Camera))
                .GetTypes()
                .Where(t => t.Name.ToLower() == (cameraDto.Type + "Camera").ToLower())
                .FirstOrDefault();

            var camera = Mapper.Map(cameraDto, cameraDto.GetType(), cameraType);

            return camera as Camera;
        }

        private static void ImportLenses(UnitOfWork unit)
        {
            string json = File.ReadAllText(LENSES_PATH);
            IEnumerable<LensDto> lensesDtos = JsonConvert
                .DeserializeObject<IEnumerable<LensDto>>(json);
            foreach (var lensDto in lensesDtos)
            {
                if (lensDto.Make == null || lensDto.FocalLength < 0 ||
                    lensDto.MaxAperture < 0 || lensDto.CompatibleWith == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                Lens lens = Mapper.Map<Lens>(lensDto);
                unit.Lenses.Add(lens);
                unit.Commit();

                Console.WriteLine($"Successfully imported {lens.Make} {lens.FocalLength}mm f{lens.MaxAperture}");
            }
        }

        private static void ConfigureMapping(UnitOfWork unit)
        {
            Mapper.Initialize(m => 
            {
                m.CreateMap<LensDto, Lens>();

                m.CreateMap<CameraDto, MirrorlessCamera>();
                m.CreateMap<CameraDto, DSLRCamera>();

                //m.CreateMap<PhotographerDto, Photographer>();
            });
        }
    }
}
