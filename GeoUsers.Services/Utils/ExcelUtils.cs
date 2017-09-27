using GeoUsers.Services.Model.Entities;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;
using System.IO;

namespace GeoUsers.Services.Utils
{
    public static class ExcelUtils
    {
        public static void ExportOrganizacionesTable(IEnumerable<Organizacion> organizaciones, string filePath)
        {
            //Create new Excel Workbook
            var workbook = new HSSFWorkbook();
            //Create new Excel Sheet
            var sheet = workbook.CreateSheet();
            //Create a header row
            var headerRow = sheet.CreateRow(0);

            headerRow.CreateCell(0).SetCellValue("Nombre");
            headerRow.CreateCell(1).SetCellValue("Dirección");
            headerRow.CreateCell(2).SetCellValue("Localidad");
            headerRow.CreateCell(3).SetCellValue("Email");
            headerRow.CreateCell(4).SetCellValue("Web");
            headerRow.CreateCell(5).SetCellValue("Teléfono");
            headerRow.CreateCell(6).SetCellValue("CUIT");
            headerRow.CreateCell(7).SetCellValue("Contact/Cargo");
            headerRow.CreateCell(8).SetCellValue("Personal");
            headerRow.CreateCell(9).SetCellValue("Usuario INTI");
            headerRow.CreateCell(10).SetCellValue("Tipo Organización");
            headerRow.CreateCell(11).SetCellValue("Rubro");

            //Freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            var rowNumber = 1;

            //Populate the sheet with values
            foreach (var organizacion in organizaciones)
            {
                //Create a new Row
                var row = sheet.CreateRow(rowNumber++);

                //Set the Values for Cells
                row.CreateCell(0).SetCellValue(organizacion.Nombre);
                row.CreateCell(1).SetCellValue(organizacion.Direccion);
                row.CreateCell(2).SetCellValue(organizacion.Localidad.Nombre);
                row.CreateCell(3).SetCellValue(organizacion.Email);
                row.CreateCell(4).SetCellValue(organizacion.Web);
                row.CreateCell(5).SetCellValue(organizacion.Telefono);
                row.CreateCell(6).SetCellValue(organizacion.Cuit.HasValue ? organizacion.Cuit.ToString() : "");
                row.CreateCell(7).SetCellValue(organizacion.ContactoCargo);
                row.CreateCell(8).SetCellValue(organizacion.Personal.HasValue ? organizacion.Personal.ToString() : "");
                row.CreateCell(9).SetCellValue(organizacion.UsuarioInti ? "Sí" : "No");
                row.CreateCell(10).SetCellValue(organizacion.TipoOrganizacion.Tipo);
                row.CreateCell(11).SetCellValue(organizacion.Rubro.Nombre);
            }

            for (var i = 0; i < 12; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            //Write the Workbook to a memory stream
            using (var output = new MemoryStream())
            {
                workbook.Write(output);

                //Save it to a physic file.
                File.WriteAllBytes(filePath, output.ToArray());
            }
        }

        public static void ExportLocalidadesTable(IEnumerable<Localidad> localidades, string filePath)
        {
            //Create new Excel Workbook
            var workbook = new HSSFWorkbook();
            //Create new Excel Sheet
            var sheet = workbook.CreateSheet();
            //Create a header row
            var headerRow = sheet.CreateRow(0);

            headerRow.CreateCell(0).SetCellValue("Nombre");
            headerRow.CreateCell(1).SetCellValue("Código Postal");

            //Freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            var rowNumber = 1;

            foreach (var localidad in localidades)
            {
                //Create a new Row
                var row = sheet.CreateRow(rowNumber++);

                //Set the Values for Cells
                row.CreateCell(0).SetCellValue(localidad.Nombre);
                row.CreateCell(1).SetCellValue(localidad.CodigoPostal);
            }

            for (var i = 0; i < 3; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            //Write the Workbook to a memory stream
            using (var output = new MemoryStream())
            {
                workbook.Write(output);

                //Save it to a physic file.
                File.WriteAllBytes(filePath, output.ToArray());
            }
        }

        public static void ExportRubrosTable(IEnumerable<Rubro> rubros, string filePath)
        {
            //Create new Excel Workbook
            var workbook = new HSSFWorkbook();
            //Create new Excel Sheet
            var sheet = workbook.CreateSheet();
            //Create a header row
            var headerRow = sheet.CreateRow(0);

            headerRow.CreateCell(0).SetCellValue("Nombre");
            headerRow.CreateCell(1).SetCellValue("Sector");

            //Freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            var rowNumber = 1;

            //Populate the sheet with values
            foreach (var rubro in rubros)
            {
                //Create a new Row
                var row = sheet.CreateRow(rowNumber++);

                //Set the Values for Cells
                row.CreateCell(0).SetCellValue(rubro.Nombre);
                row.CreateCell(11).SetCellValue(rubro.Sector.Nombre);
            }

            for (var i = 0; i < 3; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            //Write the Workbook to a memory stream
            using (var output = new MemoryStream())
            {
                workbook.Write(output);

                //Save it to a physic file.
                File.WriteAllBytes(filePath, output.ToArray());
            }
        }

        public static void ExportSectoresTable(IEnumerable<Sector> sectores, string filePath)
        {
            //Create new Excel Workbook
            var workbook = new HSSFWorkbook();
            //Create new Excel Sheet
            var sheet = workbook.CreateSheet();
            //Create a header row
            var headerRow = sheet.CreateRow(0);

            headerRow.CreateCell(0).SetCellValue("Nombre");

            //Freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            var rowNumber = 1;

            //Populate the sheet with values
            foreach (var sector in sectores)
            {
                //Create a new Row
                var row = sheet.CreateRow(rowNumber++);

                //Set the Values for Cells
                row.CreateCell(0).SetCellValue(sector.Nombre);
            }

            sheet.AutoSizeColumn(1);

            //Write the Workbook to a memory stream
            using (var output = new MemoryStream())
            {
                workbook.Write(output);

                //Save it to a physic file.
                File.WriteAllBytes(filePath, output.ToArray());
            }
        }

        public static void ExportTipoOrganizacionesTable(IEnumerable<TipoOrganizacion> tipoOrganizaciones, string filePath)
        {
            //Create new Excel Workbook
            var workbook = new HSSFWorkbook();
            //Create new Excel Sheet
            var sheet = workbook.CreateSheet();
            //Create a header row
            var headerRow = sheet.CreateRow(0);

            headerRow.CreateCell(0).SetCellValue("Tipo");

            //Freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            var rowNumber = 1;

            //Populate the sheet with values

            foreach (var tipoOrganizacion in tipoOrganizaciones)
            {
                //Create a new Row
                var row = sheet.CreateRow(rowNumber++);

                //Set the Values for Cells
                row.CreateCell(0).SetCellValue(tipoOrganizacion.Tipo);
            }

            sheet.AutoSizeColumn(1);

            //Write the Workbook to a memory stream
            using (var output = new MemoryStream())
            {
                workbook.Write(output);

                //Save it to a physic file.
                File.WriteAllBytes(filePath, output.ToArray());
            }
        }
    }
}
