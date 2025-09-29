const wind = window;
const sticky = document.querySelector('#sticky-header');

const handleSticky = () => {
    const scroll = wind.scrollY;
    const screenWidth = wind.innerWidth;
    if (screenWidth >= 992) {
        sticky.classList.remove('mobile-header');
        if (scroll > 90) {
            sticky.classList.add('sticky')
        } else {
            sticky.classList.remove('sticky')
        }
    } else {
        sticky.classList.remove('sticky')
        sticky.classList.add('mobile-header')
    }
};

// Spusti pri scrollovaní
wind.addEventListener('scroll', handleSticky);

// Spusti aj pri zmene veľkosti okna
wind.addEventListener('resize', handleSticky);

// Spusti hneď po načítaní (napr. ak je stránka otvorená už s menším scrollom)
document.addEventListener('DOMContentLoaded', handleSticky);
window.addEventListener('load', handleSticky);
