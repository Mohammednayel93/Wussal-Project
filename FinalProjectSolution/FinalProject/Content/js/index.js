$(document).ready(function(){
    $(function () {
   $(document).scroll(function () {
     var $nav = $("header");
     var $ddl = $("header .dropdown .dropdown-menu");
     $nav.toggleClass('scrolled', $(this).scrollTop() > $nav.height());
     $ddl.toggleClass('scrolled', $(this).scrollTop() > $nav.height());
   });
 });
});
$('.worksUL ul li').on('click',function(){
  $(this).addClass('activeLi').siblings().removeClass('activeLi');
  if($(this).data('class')==='all'){
      $('.product').fadeIn(200);

  }else{
      $('.product').hide(); 
      $($(this).data('class')).parent().fadeIn(200);
  }
})
$('.searchBtn').on('click',function(){
  $('.filterDiv').css('display','none')
  $('.searchDiv').slideToggle(500);
})
$('.filterBtn').on('click',function(){
  $('.searchDiv').css('display','none');
  $('.filterDiv').slideToggle(500);
})
var swiper = new Swiper('.swiper-container', {
  effect: 'coverflow',
  grabCursor: true,
  centeredSlides: true,
  slidesPerView: 'auto',
  coverflowEffect: {
    rotate: 50,
    stretch: 0,
    depth: 100,
    modifier: 1,
    slideShadows: true,
  },
  pagination: {
    el: '.swiper-pagination',
  },
});