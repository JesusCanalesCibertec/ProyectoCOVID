package net.spring.springreporterserver;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.ComponentScan;

@SpringBootApplication
@ComponentScan(value = { "net.spring" })
public class SpringReporterServerApplication  {

	public static void main(String[] args) {
		SpringApplication.run(SpringReporterServerApplication.class, args);
	}
}
