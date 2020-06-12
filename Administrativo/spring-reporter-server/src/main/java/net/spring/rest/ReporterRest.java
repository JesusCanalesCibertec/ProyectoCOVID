package net.spring.rest;

import java.util.Map;

import javax.sql.DataSource;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import net.sf.jasperreports.engine.JasperRunManager;
import net.spring.dominio.dto.DtoReporte;

@RestController
@RequestMapping("/spring/reporter")
@CrossOrigin(origins = "*")
@Transactional
public class ReporterRest {

	@Autowired
	private DataSource dataSource;

	@RequestMapping(method = RequestMethod.POST, value = "/pdf")
	public DtoReporte pdf2(@RequestBody Map<String, Object> parameters) {

		DtoReporte dtoReporte = new DtoReporte();

		try {
			dtoReporte.datos = JasperRunManager.runReportToPdf(parameters.get("P_REPORTE").toString().replace("|", "/"), parameters,
					dataSource.getConnection());
		} catch (Exception ex) {
			dtoReporte.excepcion = ex.getMessage();
		}

		return dtoReporte;
	}

}
