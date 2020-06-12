using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.correo.dominio;
using net.royal.spring.sistema.constante;
using net.royal.spring.sistema.dao;
using net.royal.spring.framework.core;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace net.royal.spring.sistema.servicio.impl
{
    public class SyCorreoServicioImpl : SyCorreoServicio
    {
        private IServiceProvider servicioProveedor;
        private ParametrosmastDao parametrosmastDao;
        private ILogger _logger;

        public SyCorreoServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            parametrosmastDao = servicioProveedor.GetService<ParametrosmastDao>();
            _logger = servicioProveedor.GetService<ILoggerFactory>().CreateLogger(this.GetType().Namespace + "." + this.GetType().Name);
        }
        public void enviarCorreInmediato(EmailConfiguracion config, List<Email> listaEmail)
        {
            _logger.LogInformation("=======> antes de enviar correos");
            foreach (Email bean in listaEmail)
            {
                try
                {
                    _logger.LogInformation(bean.CorreoDestinoPara);
                    MailMessage mail = new MailMessage();
                    if (UString.estaVacio(bean.Remitente))
                    {
                        mail.From = new MailAddress(config.EmailCuenta);
                    }
                    else
                    {
                        mail.From = new MailAddress(bean.Remitente);
                    }

                    if (config.FlgCorreoPrueba.Equals("S"))
                    {
                        mail.To.Add(new MailAddress(config.CorreoPrueba));
                        _logger.LogInformation(config.CorreoPrueba);
                    }
                    else
                    {
                        if (!UValidador.esListaVacia(bean.ListaCorreoDestino))
                        {
                            var c = bean.ListaCorreoDestino[0];
                            mail.To.Add(new MailAddress(c.CorreoDestino));
                            _logger.LogInformation(c.CorreoDestino);
                        }
                        if (!UString.estaVacio(bean.CorreoDestinoCopia))
                            mail.CC.Add(new MailAddress(bean.CorreoDestinoCopia));
                        if (!UString.estaVacio(bean.CorreoDestinoCopiaOculta))
                            mail.Bcc.Add(new MailAddress(bean.CorreoDestinoCopiaOculta));
                    }
                    mail.Subject = bean.Asunto;
                    if (bean.CuerpoCorreo != null)
                    {
                        mail.Body = UByte.convertirString(bean.CuerpoCorreo);
                    }

                    mail.IsBodyHtml = true;
                    //mail.Priority = MailPriority.High;
                    _logger.LogInformation("==> antes del envio");
                    enviar(mail, config);
                    _logger.LogInformation("==> despues del envio");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    _logger.LogError(ex.Source);
                    _logger.LogError(ex.StackTrace);
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void enviarCorreInmediatoAsincrono(EmailConfiguracion config, List<Email> listaEmail)
        {
            _logger.LogInformation("=======> antes de enviar correos : asincrono");
            foreach (Email bean in listaEmail)
            {
                try
                {
                    _logger.LogInformation(bean.CorreoDestinoPara);
                    MailMessage mail = new MailMessage();
                    if (UString.estaVacio(bean.Remitente))
                    {
                        mail.From = new MailAddress(config.EmailCuenta);
                    }
                    else
                    {
                        mail.From = new MailAddress(bean.Remitente);
                    }

                    if (config.FlgCorreoPrueba.Equals("S"))
                    {
                        mail.To.Add(new MailAddress(config.CorreoPrueba));
                        _logger.LogInformation(config.CorreoPrueba);
                    }
                    else
                    {
                        if (!UValidador.esListaVacia(bean.ListaCorreoDestino))
                        {
                            var c = bean.ListaCorreoDestino[0];
                            mail.To.Add(new MailAddress(c.CorreoDestino));
                            _logger.LogInformation(c.CorreoDestino);
                        }
                        if (!UString.estaVacio(bean.CorreoDestinoCopia))
                            mail.CC.Add(new MailAddress(bean.CorreoDestinoCopia));
                        if (!UString.estaVacio(bean.CorreoDestinoCopiaOculta))
                            mail.Bcc.Add(new MailAddress(bean.CorreoDestinoCopiaOculta));
                    }
                    mail.Subject = bean.Asunto;
                    if (bean.CuerpoCorreo != null)
                    {
                        mail.Body = UByte.convertirString(bean.CuerpoCorreo);
                    }

                    mail.IsBodyHtml = true;
                    //mail.Priority = MailPriority.High;
                    _logger.LogInformation("==> antes del envio");
                    enviarCorreo(mail, config);
                    _logger.LogInformation("==> despues del envio");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    _logger.LogError(ex.Source);
                    _logger.LogError(ex.StackTrace);
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public EmailConfiguracion obtenerConfiguracion()
        {
            String valor = null;

            EmailConfiguracion config = new EmailConfiguracion();

            valor = parametrosmastDao.obtenerValorExplicacion(ConstanteCorreo.PARAMETRO_TIPO_CONFIGURACION,
                    ConstanteCorreo.APLICACION_CODIGO);
            if (UString.estaVacio(valor))
                valor = ConstanteCorreo.CORREO_TIPO_CONFIGURACION_CLASE;
            config.TipoConfiguracion = valor;

            valor = parametrosmastDao.obtenerValorExplicacion(ConstanteCorreo.PARAMETRO_MAIL_CUENTA,
                    ConstanteCorreo.APLICACION_CODIGO);
            config.EmailCuenta = valor;

            String cuenta = valor;

            valor = parametrosmastDao.obtenerValorExplicacion(ConstanteCorreo.PARAMETRO_MAIL_CLAVE,
                    ConstanteCorreo.APLICACION_CODIGO);
            config.EmailClave = valor;

            String clave = valor;

            valor = parametrosmastDao.obtenerValorExplicacion(ConstanteCorreo.PARAMETRO_MAIL_PERFIL,
                    ConstanteCorreo.APLICACION_CODIGO);
            config.EmailPerfil = valor;

            valor = parametrosmastDao.obtenerValorExplicacion(ConstanteCorreo.PARAMETRO_MAIL_ES_CORREO_PRUEBA,
                    ConstanteCorreo.APLICACION_CODIGO);
            if (UString.estaVacio(valor))
                valor = "S";
            config.FlgCorreoPrueba = valor;

            valor = parametrosmastDao.obtenerValorExplicacion(ConstanteCorreo.PARAMETRO_MAIL_CUENTA_CORREO_PRUEBA,
                    ConstanteCorreo.APLICACION_CODIGO);
            config.CorreoPrueba = valor;

            valor = parametrosmastDao.obtenerValorExplicacion(ConstanteCorreo.PARAMETRO_MAIL_RUTA_TEMPORAL,
                    ConstanteCorreo.APLICACION_CODIGO);
            config.RutaRaizAdjuntos = valor;
            /***********/
            valor = parametrosmastDao.obtenerValorExplicacion(ConstanteCorreo.PARAMETRO_MAIL_FLAG_SSL,
                    ConstanteCorreo.APLICACION_CODIGO);
            if (UString.estaVacio(valor))
                valor = "N";
            config.EmailFlagSsl = valor;

            valor = parametrosmastDao.obtenerValorExplicacion(ConstanteCorreo.PARAMETRO_MAIL_HOST,
                    ConstanteCorreo.APLICACION_CODIGO);
            config.EmailHost = valor;

            valor = parametrosmastDao.obtenerValorExplicacion(ConstanteCorreo.PARAMETRO_MAIL_PUERTO,
                    ConstanteCorreo.APLICACION_CODIGO);
            config.EmailPuerto = valor;
            /************/
            valor = parametrosmastDao.obtenerValorExplicacion(ConstanteCorreo.PARAMETRO_MAIL_ENVIO_ASINCRONO,
                    ConstanteCorreo.APLICACION_CODIGO);
            if (UString.estaVacio(valor))
                valor = "N";
            config.EnvioAsincrono = valor;


            valor = parametrosmastDao.obtenerValorExplicacion(ConstanteCorreo.PARAMETRO_MAIL_ENVIO,
                    ConstanteCorreo.APLICACION_CODIGO);
            if (UString.estaVacio(valor))
                valor = "N";
            config.FlgEnviar = valor;

            return config;
        }

        public void enviarCorreo(MailMessage mensaje, EmailConfiguracion conf)
        {
            System.Threading.Thread threadSendMails;
            threadSendMails = new System.Threading.Thread(delegate () { enviar(mensaje, conf); });
            threadSendMails.IsBackground = true;
            threadSendMails.Start();
        }
        public void enviar(MailMessage mensaje, EmailConfiguracion conf)
        {
            SmtpClient smtp = new SmtpClient(conf.EmailHost, Int32.Parse(conf.EmailPuerto));
            smtp.Credentials = new NetworkCredential(conf.EmailCuenta, conf.EmailClave);

            if (conf.EmailFlagSsl.Equals("S"))
            {
                smtp.EnableSsl = true;
            }
            else
                smtp.EnableSsl = false;
            try
            {
                smtp.Send(mensaje);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.Source);
                _logger.LogError(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }

        }
    }
}
