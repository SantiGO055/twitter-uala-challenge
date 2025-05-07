
# Twitter Ualá Challenge

### La api se encuentra hosteada en la siguiente url:

[https://twitter-uala-challenge.onrender.com/swagger/index.html](https://twitter-uala-challenge.onrender.com/swagger/index.html)

- Posee datos mockeados de varios usuarios para facilitar las pruebas.

## Para levantar la Aplicación con Docker (Local) 🐳

### 📋 Requisitos Previos

- Docker instalado ([Descargar aquí](https://www.docker.com/get-started))
- Docker Compose (viene incluido con Docker Desktop)
- Archivo .env configurado (asegurate de tenerlo en la raíz del proyecto) 

⚙️ Configuración del archivo .env debe contener las siguientes variables:
```env
# Database (PostgreSQL - Supabase)
PGHOST=aws-0-sa-east-1.pooler.supabase.com
PGPORT=5432
PGDATABASE=postgres
PGPASSWORD=twitterualachallenge123
PGUSER=postgres.uouglzizdzzlneqlkwgp

# API Config
SWAGGER_ENABLED=true
ASPNETCORE_ENVIRONMENT=Development
ALLOWED_ORIGINS=http://localhost:3000,http://127.0.0.1:3000,http:localhost:3000,http://127.0.0.1:3000,http://127.0.0.1:3001
ALLOWED_METHODS=GET,POST,PUT,DELETE,OPTIONS,DELETE
SHOW_EXCEPTION_DETAIL=true
```
## 🚀 Pasos para Levantar la Aplicación
1. Construir la imágen de Docker

```bash
docker-compose build
```

2. Iniciar los contenedores
```bash
docker-compose up -d
```

Nota: Si se utiliza supabase (externo), no hace falta levantar una DB local.

🔍 Verificar el funcionamiento de la API:
- Abrir navegador y visíta: http://localhost/swagger

***

## Para levantar la Aplicación sin Docker (Local) 🖥️
### 📋 Requisitos Previos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

1. Clonar el repositorio

```bash
   git clone https://github.com/SantiGO055/twitter-uala-challenge.git
   cd twitter-uala-challenge
```
2. Crear carpeta y archivo launchSettings.json dentro de TwitterUalaChallenge.API
```bash
    cd TwitterUalaChallenge.API
    mkdir Properties
    cd Properties
    touch launchSettings.json
```
3. Pegar el contenido del json:

```json
{
    "profiles": {
        "TwitterUalaChallenge": {
            "commandName": "Project",
            "launchBrowser": false,
            "launchUrl": "swagger",
            "applicationUrl": "http://localhost:3000",
                "environmentVariables": {
                    "ASPNETCORE_ENVIRONMENT": "Development",
                    "ALLOWED_METHODS": "GET,POST,PUT,DELETE,OPTIONS,DELETE",
                    "ALLOWED_ORIGINS": "http://localhost:3000,http://127.0.0.1:3000,http:localhost:3000,http://127.0.0.1:3000,http://127.0.0.1:3001",
                    "PGHOST": "aws-0-sa-east-1.pooler.supabase.com",
                    "PGPORT": "5432",
                    "PGDATABASE": "postgres",
                    "PGUSER": "postgres.uouglzizdzzlneqlkwgp",
                    "PGPASSWORD": "twitterualachallenge123",
                    "SWAGGER_ENABLED": "true",
                    "SHOW_EXCEPTION_DETAIL": "true",
                    "MINIMUM_LOG_LEVEL": "Trace"
                }
        }
    }
}
```

4. Instalar dependencias
```bash
    cd ..
   dotnet restore
```
5. Ejecutar la aplicación
```bash
   dotnet run
```