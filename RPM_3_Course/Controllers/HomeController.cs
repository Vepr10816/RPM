using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RPM_3_Course.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace RPM_3_Course.Controllers
{
    public class HomeController : Controller
    {
        
        private ApplicationContext db;

        static int IdUser;
        private IWebHostEnvironment _app;
        public HomeController(ApplicationContext context, IWebHostEnvironment app)
        {
            db = context;
            _app = app;
        }


        /*public IActionResult Auth()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Auth(User user)
        {
            if(user!=null)
            {
                var UserLog = db.Users.FirstOrDefault(predicate => predicate.Login == user.Login && predicate.Password == user.Password);
                if(UserLog!=null)
                {
                    HttpContext.Session.SetString("Login",UserLog.Login);
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }*/

        public IActionResult AddFile()
        {
            return View(db.Picturee.ToList());
        }


        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile file)
        {
            if(file != null)
            {
                string path = "/Files/" + file.FileName;
                using (FileStream fileStream = new FileStream
                    (_app.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                Picturee fileModel = new Picturee
                {
                    Name_Picture = file.FileName,
                    Path = path
                };
                db.Picturee.Add(fileModel);
                await db.SaveChangesAsync();
                var pi = db.Picturee.ToList();

                Picturee picturee = pi.LastOrDefault(p => p.Path == path);
                User user = db.Users.Find(Convert.ToInt32(IdUser));
                user.PictureeId = picturee.Id;
                db.SaveChanges();
                return RedirectToAction("Profile");
            }
            return RedirectToAction("AddFile");


        }

        public async Task<IActionResult> Index(int? id, string email, int page = 1, SortState sortOrder = SortState.IdAsc)
        {
            /*ViewBag.Login = HttpContext.Session.GetString("Login");*/
            IQueryable<User> users = db.Users;
            //фильтрация или поиск
            if (id != null && id > 0)
            {
                users = users.Where(p => p.Id == id);
            }
            if (!String.IsNullOrEmpty(email))
            {
                users = users.Where(p => p.Email.Contains(email));
            }
            //Сортировка
            switch (sortOrder)
            {
                case SortState.IdAsc:
                    {
                        users = users.OrderBy(p => p.Id);
                        break;
                    }
                case SortState.IdDesc:
                    {
                        users = users.OrderByDescending(p => p.Id);
                        break;
                    }
                case SortState.EmailAsc:
                    {
                        users = users.OrderBy(p => p.Email);
                        break;
                    }
                case SortState.EmailDesc:
                    {
                        users = users.OrderByDescending(p => p.Email);
                        break;
                    }
            }
            //Пагинация
            int pageSize = 3;
            var count = await users.CountAsync();
            var item = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            IndexViewModel viewModel = new IndexViewModel
            {
                FilterViewModel = new FilterViewModel(id, email),
                SortViewModel = new SortViewModel(sortOrder),
                PageViewModel = new PageViewModel(count, page, pageSize),
                Users = item
            };
            return View(viewModel);
        }


        public async Task<IActionResult> IndexPicture(int? id, string email, int page = 1, SortState sortOrder = SortState.IdAsc)
        {
            IQueryable<Picturee> users = db.Picturee;
            //фильтрация или поиск
            if (id != null && id > 0)
            {
                users = users.Where(p => p.Id == id);
            }
            if (!String.IsNullOrEmpty(email))
            {
                users = users.Where(p => p.Name_Picture.Contains(email));
            }
            //Сортировка
            switch (sortOrder)
            {
                case SortState.IdAsc:
                    {
                        users = users.OrderBy(p => p.Id);
                        break;
                    }
                case SortState.IdDesc:
                    {
                        users = users.OrderByDescending(p => p.Id);
                        break;
                    }
                case SortState.EmailAsc:
                    {
                        users = users.OrderBy(p => p.Name_Picture);
                        break;
                    }
                case SortState.EmailDesc:
                    {
                        users = users.OrderByDescending(p => p.Name_Picture);
                        break;
                    }
            }
            //Пагинация
            int pageSize = 3;
            var count = await users.CountAsync();
            var item = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            IndexPictureViewModel viewModel = new IndexPictureViewModel
            {
                FilterViewModel = new FilterViewModel(id, email),
                SortViewModel = new SortViewModel(sortOrder),
                PageViewModel = new PageViewModel(count, page, pageSize),
                Picturee = item
            };
            return View(viewModel);
        }


        public async Task<IActionResult> IndexPostPicture(int? id, string email, int page = 1, SortState sortOrder = SortState.IdAsc)
        {
            IQueryable<PostPicture> users = db.PostPicture;
            //фильтрация или поиск
            if (id != null && id > 0)
            {
                users = users.Where(p => p.Id == id);
            }
            if (!String.IsNullOrEmpty(email))
            {
                users = users.Where(p => p.Name_Picture.Contains(email));
            }
            //Сортировка
            switch (sortOrder)
            {
                case SortState.IdAsc:
                    {
                        users = users.OrderBy(p => p.Id);
                        break;
                    }
                case SortState.IdDesc:
                    {
                        users = users.OrderByDescending(p => p.Id);
                        break;
                    }
                case SortState.EmailAsc:
                    {
                        users = users.OrderBy(p => p.Name_Picture);
                        break;
                    }
                case SortState.EmailDesc:
                    {
                        users = users.OrderByDescending(p => p.Name_Picture);
                        break;
                    }
            }
            //Пагинация
            int pageSize = 3;
            var count = await users.CountAsync();
            var item = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            IndexPostPictureViewModel viewModel = new IndexPostPictureViewModel
            {
                FilterViewModel = new FilterViewModel(id, email),
                SortViewModel = new SortViewModel(sortOrder),
                PageViewModel = new PageViewModel(count, page, pageSize),
                PostPicture = item
            };
            return View(viewModel);
        }


        public async Task<IActionResult> IndexPost(int? id, string email, int page = 1, SortState sortOrder = SortState.IdAsc)
        {
            IQueryable<Post> users = db.Post;
            //фильтрация или поиск
            if (id != null && id > 0)
            {
                users = users.Where(p => p.Id == id);
            }
            if (!String.IsNullOrEmpty(email))
            {
                users = users.Where(p => p.Title.Contains(email));
            }
            //Сортировка
            switch (sortOrder)
            {
                case SortState.IdAsc:
                    {
                        users = users.OrderBy(p => p.Id);
                        break;
                    }
                case SortState.IdDesc:
                    {
                        users = users.OrderByDescending(p => p.Id);
                        break;
                    }
                case SortState.EmailAsc:
                    {
                        users = users.OrderBy(p => p.Title);
                        break;
                    }
                case SortState.EmailDesc:
                    {
                        users = users.OrderByDescending(p => p.Title);
                        break;
                    }
            }
            //Пагинация
            int pageSize = 3;
            var count = await users.CountAsync();
            var item = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            IndexPostViewModel viewModel = new IndexPostViewModel
            {
                FilterViewModel = new FilterViewModel(id, email),
                SortViewModel = new SortViewModel(sortOrder),
                PageViewModel = new PageViewModel(count, page, pageSize),
                Post = item
            };
            return View(viewModel);
        }



        public IActionResult Create()
        {
            return View();
        }
        public IActionResult CreatePicture()
        {
            return View();
        }
        public IActionResult CreatePostPicture()
        {
            return View();
        }
        public IActionResult CreatePost()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult Sign_In()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> CreatePicture(Picturee user)
        {
            db.Picturee.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexPicture");
        }


        [HttpPost]
        public async Task<IActionResult> CreatePostPicture(PostPicture user)
        {
            db.PostPicture.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexPostPicture");
        }


        [HttpPost]
        public async Task<IActionResult> CreatePost(Post post)
        {
            db.Post.Add(post);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexPost");
        }


        [HttpGet]
        [ActionName("Delete")]
        public async Task<ActionResult> ConfimDelete(int? id)
        {
            if(id !=null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if(user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                    
                }
            }
            return NotFound();
        }


        [HttpGet]
        [ActionName("DeletePicture")]
        public async Task<ActionResult> ConfimDeletePicture(int? id)
        {
            if (id != null)
            {
                Picturee user = await db.Picturee.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> DeletePicture(int? id)
        {
            if (id != null)
            {
                Picturee user = await db.Picturee.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    db.Picturee.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexPicture");

                }
            }
            return NotFound();
        }


        [HttpGet]
        [ActionName("DeletePostPicture")]
        public async Task<ActionResult> ConfimDeletePostPicture(int? id)
        {
            if (id != null)
            {
                PostPicture user = await db.PostPicture.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> DeletePostPicture(int? id)
        {
            if (id != null)
            {
                PostPicture user = await db.PostPicture.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    db.PostPicture.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexPostPicture");

                }
            }
            return NotFound();
        }


        [HttpGet]
        [ActionName("DeletePost")]
        public async Task<ActionResult> ConfimDeletePost(int? id)
        {
            if (id != null)
            {
                Post user = await db.Post.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> DeletePost(int? id)
        {
            if (id != null)
            {
                Post user = await db.Post.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    db.Post.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexPost");

                }
            }
            return NotFound();
        }




        public async Task<ActionResult> Edit(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            if(IdUser>0)
                return RedirectToAction("Profile");
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> EditPicture(int? id)
        {
            if (id != null)
            {
                Picturee user = await db.Picturee.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditPicture(Picturee user)
        {
            db.Picturee.Update(user);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexPicture");
        }


        public async Task<ActionResult> EditPostPicture(int? id)
        {
            if (id != null)
            {
                PostPicture user = await db.PostPicture.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditPostPicture(PostPicture user)
        {
            db.PostPicture.Update(user);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexPostPicture");
        }


        public async Task<ActionResult> EditPost(int? id)
        {
            if (id != null)
            {
                Post user = await db.Post.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(Post post)
        {
            db.Post.Update(post);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexPost");
        }


        [HttpPost]
        public async Task<IActionResult> Registration(User user)
        {
            bool Log = false;
            bool Em = false;
            var users = db.Users.Where(p => EF.Functions.Like(p.Login, user.Login));
            foreach (User user2 in users)
            {
                Log = true;
            }
            users = db.Users.Where(p => EF.Functions.Like(p.Email, user.Email));
            foreach (User user2 in users)
            {
                Em = true;
            }
            if (Log == true || Em == true)
            {
                return RedirectToAction("Registration");
            }
            else
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Sign_In(User user)
        {
            int ff = 0;
            bool login1 = false;
            bool login2 = false;
            var users = db.Users.Where(p => EF.Functions.Like(p.Login, user.Login, user.Email) && EF.Functions.Like(p.Password, user.Password) && EF.Functions.Like(p.RolesId.ToString(), "1"));
            foreach (User user2 in users)
            {
                ff = user2.Id;
                login1 = true;
            }

            users = db.Users.Where(p => EF.Functions.Like(p.Login, user.Login, user.Email) && EF.Functions.Like(p.Password, user.Password) && EF.Functions.Like(p.RolesId.ToString(), "2"));

            foreach (User user2 in users)
            {
                ff = user2.Id;
                login2 = true;
            }

            if (login1 == true)
            {

                return RedirectToAction("Index");

            }

            if (login2 == true)
            {
                IdUser = ff;
                return RedirectToAction("Profile");

            }

            else
                return View();

        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }


        public async Task<ActionResult> DetailsPicture(int? id)
        {
            if (id != null)
            {
                Picturee user = await db.Picturee.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }


        public async Task<ActionResult> DetailsPostPicture(int? id)
        {
            if (id != null)
            {
                PostPicture user = await db.PostPicture.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }

        public async Task<ActionResult> DetailsPost(int? id)
        {
            if (id != null)
            {
                Post user = await db.Post.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }


        public async Task<ActionResult> Profile(int? id)
        {

            User user1 = db.Users.Find(IdUser);
            int idpic = user1.PictureeId;

            Picturee picturee = db.Picturee.Find(idpic);
            string picpath = picturee.Path;

            ViewBag.Message = picpath;

                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.Id == IdUser);
                if (user != null)
                {
                    return View(user);
                }
            
            return NotFound();
        }

        public IActionResult PostProfileCreate()
        {

            var max = db.PostPicture.Max(i => i.Id);
            int ggg = max + 1;

            ViewBag.MessagePost = ggg;
            ViewBag.Date = Convert.ToString(DateTime.Now);
            ViewBag.Useriidd = IdUser;


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> PostProfileCreate(IFormFile uploadedFile, Post post)
        {
            bool yes = false;

            if (uploadedFile != null)
            {
                // путь к папке Files

                string path = "/Files/" + Path.GetFileName(uploadedFile.FileName);

                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_app.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);

                }
                //Добавление в БД

                PostPicture file = new PostPicture { Name_Picture = Path.GetFileName(uploadedFile.FileName), Path = path };
                db.PostPicture.Add(file);
                db.SaveChanges();

                yes = true;

                if (yes == true)
                {
                    db.Post.Add(post);
                    await db.SaveChangesAsync();
                }

            }

            return RedirectToAction("Profile");
        }


        public IActionResult PostProfileView()
        {
            int ha = IdUser;


            var players = db.Post.Include(p => p.PostPicture).Where(p => p.UserId == ha); ;
            return View(players.ToList());
        }



        
        public async Task<ActionResult> DeletePostProfile(int? id)
        {
            if (id != null)
            {
                Post user = await db.Post.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    db.Post.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("PostProfileView");

                }
            }
            return NotFound();
        }


        public async Task<ActionResult> EditPostProfile(int? id)
        {
            
            if (id != null)
            {

                Post user = await db.Post.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    ViewBag.Date = Convert.ToString(DateTime.Now);
                    return View(user);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditPostProfile(Post post, IFormFile file)
        {
            /*if (file != null)
            {
                string path = "/Files/" + file.FileName;
                using (FileStream fileStream = new FileStream
                    (_app.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                PostPicture fileModel = new PostPicture
                {
                    Name_Picture = file.FileName,
                    Path = path
                };
                db.PostPicture.Add(fileModel);
                await db.SaveChangesAsync();
                var pi = db.PostPicture.ToList();

                PostPicture picturee = pi.LastOrDefault(p => p.Path == path);
                Post user = db.Post.Find(Convert.ToInt32(id));
                user.PostPictureId = picturee.Id;
                db.SaveChanges();
            }*/
            db.Post.Update(post);
            await db.SaveChangesAsync();
            return RedirectToAction("PostProfileView");
        }


        public async Task<IActionResult> PostsView(int? id)
        {
            IQueryable<Post> users = db.Post;
            IQueryable<PostPicture> users2 = db.PostPicture;
            IQueryable<User> users3 = db.Users;
            var item = await users.ToListAsync();
            /*Picturee picturee = db.Picturee.Find(idpic);
            string picpath = picturee.Path;*/
            var item2 = await users2.ToListAsync();
            var item3 = await users3.ToListAsync();
            //ViewBag.Message = picpath;
            PostProfileView viewModel = new PostProfileView
            {
                posts = item,
                pictureeposts = item2,
                users = item3

            };
            return View(viewModel);
        }


        public async Task<ActionResult> EditUser(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> EditUser(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Profile");
        }


        public async Task<ActionResult> Friend(int? id)
        {
            if ((int)id != IdUser)
            {
                User user1 = db.Users.Find(id);
                int idpic = user1.PictureeId;

                Picturee picturee = db.Picturee.Find(idpic);
                string picpath = picturee.Path;

                ViewBag.Message = picpath;

                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                    // return RedirectToAction("ProfileView", user);
                }
            }
            else
            {
                return RedirectToAction("Profile", IdUser);
            }

            return NotFound();
        }


        public IActionResult PostFriendView(int? id)
        {
                var players = db.Post.Include(p => p.PostPicture).Where(p => p.UserId == id);
                return View(players.ToList());
            
        }

    }
}
