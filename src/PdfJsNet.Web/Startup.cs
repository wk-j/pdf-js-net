using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace PdfJsNet.Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseHsts();
            }

            var asm = typeof(Viewer).Assembly;
            var asmName = asm.GetName().Name;

            var defaultOptions = new DefaultFilesOptions();
            defaultOptions.DefaultFileNames.Clear();
            defaultOptions.DefaultFileNames.Add("index.html");

            var wwwroot = new PhysicalFileProvider(env.WebRootPath);
            var compositeProvider = new CompositeFileProvider(wwwroot, new EmbeddedFileProvider(asm, $"{asmName}.viewer"));

            app
              .UseDefaultFiles(defaultOptions)
              .UseStaticFiles(new StaticFileOptions {
                  FileProvider = compositeProvider
              });

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
