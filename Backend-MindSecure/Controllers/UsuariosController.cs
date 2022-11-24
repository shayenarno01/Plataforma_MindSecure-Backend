using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend_MindSecure.Models;
using static Backend_MindSecure.Models.UserRegister;
using System.Security.Cryptography;
using static Backend_MindSecure.Models.Login;

namespace Backend_MindSecure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly dbplataformasaludtdsContext _context;

        
        public UsuariosController(dbplataformasaludtdsContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios

        [HttpPost ("Registro")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (_context.Usuarios.Any(u => u.Email == request.Email))
            {
                return BadRequest("User already exists.");
            }

            CrearClaveHash(request.Clave,
                 out byte[] passwordHash,
                 out byte[] passwordSalt);

            var usuarios = new Usuario
            {
                Email = request.Email,
                Clave = request.Clave,
                Usuario1 = request.Usuario1,
                Telefono = request.Telefono,
                Rol = request.Rol,
                Status = request.Status
                //PasswordHash = passwordHash,
                //PasswordSalt = passwordSalt,
                //VerificationToken = CreateRandomToken()
            };

            _context.Usuarios.Add(usuarios);
            await _context.SaveChangesAsync();

            return Ok("User successfully created!");
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        private void CrearClaveHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
//            Usuario usuario = new Usuario();

            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }else if(user.Clave !=  request.Clave ){

                return BadRequest("Clave incorrecta.");


            }

            //if ( user.Clave && user.Email != (request.Clave, request.Email))
            //{
            //    return BadRequest("Password is incorrect.");
            //}

            //if (user.VerifiedAt == null)
            //{
            //    return BadRequest("Not verified!");
            //}

            return Ok($"Welcome back, {user.Email}! :)");
        }


        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
