# CategoryListSection Carousel Component

A responsive, accessible carousel component for displaying category items with smooth navigation and modern UI design.

## Features

- 🎠 **Smooth Carousel Navigation** - Left/right arrow navigation with smooth scrolling
- 📱 **Fully Responsive** - Adapts to different screen sizes automatically
- ♿ **Accessibility** - ARIA labels, keyboard navigation, and focus management
- 🎯 **Touch Friendly** - Supports touch gestures and mouse wheel scrolling
- 🎨 **Modern Design** - Clean, card-based layout with hover effects
- 📊 **Dots Indicator** - Visual progress indicator for carousel position
- ⚡ **Performance Optimized** - Lazy loading images and efficient scrolling

## Props

| Prop | Type | Default | Description |
|------|------|---------|-------------|
| `categories` | `Category[]` | Required | Array of category objects to display |
| `title` | `string` | "Shop with Categories" | Section title displayed above the carousel |
| `itemsPerView` | `number` | 6 | Number of items visible at once on desktop |

## Category Interface

```typescript
interface Category {
  id: string;
  name: string;
  image: string;
  description?: string;
}
```

## Basic Usage

```tsx
import { CategoryListSection } from './CategoryListSection';

const MyComponent = () => {
  const categories = [
    {
      id: '1',
      name: 'Electronics',
      image: '/path/to/image.jpg',
      description: 'Latest electronic devices'
    },
    // ... more categories
  ];

  return (
    <CategoryListSection 
      categories={categories}
      title="Shop by Category"
    />
  );
};
```

## Advanced Usage

### Custom Items Per View

```tsx
<CategoryListSection 
  categories={categories}
  title="Featured Categories"
  itemsPerView={4}
/>
```

### With Async Data

```tsx
const [categories, setCategories] = useState<Category[]>([]);
const [loading, setLoading] = useState(true);

useEffect(() => {
  fetchCategories().then(setCategories).finally(() => setLoading(false));
}, []);

if (loading) return <div>Loading...</div>;

return (
  <CategoryListSection 
    categories={categories}
    title="Dynamic Categories"
  />
);
```

### Handle Category Clicks

```tsx
const handleCategoryClick = (categoryId: string) => {
  // Navigate to category page
  router.push(`/category/${categoryId}`);
};

return (
  <div onClick={(e) => {
    const target = e.target as HTMLElement;
    const categoryCard = target.closest('.category-card');
    if (categoryCard) {
      const categoryId = categoryCard.getAttribute('data-category-id');
      if (categoryId) {
        handleCategoryClick(categoryId);
      }
    }
  }}>
    <CategoryListSection 
      categories={categories}
      title="Clickable Categories"
    />
  </div>
);
```

## Navigation

### Arrow Navigation
- **Left Arrow**: Navigate to previous set of categories
- **Right Arrow**: Navigate to next set of categories
- Arrows are automatically disabled when reaching the start/end

### Touch & Mouse
- **Mouse Wheel**: Scroll through categories
- **Touch Swipe**: Swipe left/right on mobile devices
- **Click Navigation**: Click on dots indicator to jump to specific position

## Responsive Behavior

| Screen Size | Items Per View | Navigation |
|-------------|----------------|------------|
| Desktop (>1024px) | 6 | Full arrows + dots |
| Tablet (768px-1024px) | 5 | Medium arrows + dots |
| Mobile (640px-768px) | 4 | Small arrows + dots |
| Small Mobile (<640px) | 3 | Compact arrows + dots |
| Tiny Mobile (<480px) | 2 | Minimal arrows + dots |

## Styling

The component uses CSS modules and follows the project's design system:

- **Primary Color**: `#ff6b35` (Orange)
- **Text Colors**: Dark grays for readability
- **Shadows**: Subtle shadows for depth
- **Transitions**: Smooth 0.3s ease transitions
- **Border Radius**: 12px for cards, 8px for images

## Accessibility Features

- **ARIA Labels**: Proper labeling for navigation buttons
- **Keyboard Navigation**: Tab-able navigation elements
- **Focus Management**: Clear focus indicators
- **Screen Reader Support**: Semantic HTML structure
- **High Contrast**: Accessible color combinations

## Browser Support

- ✅ Chrome 60+
- ✅ Firefox 55+
- ✅ Safari 12+
- ✅ Edge 79+
- ✅ Mobile browsers (iOS Safari, Chrome Mobile)

## Performance Tips

1. **Image Optimization**: Use appropriately sized images (300x200px recommended)
2. **Lazy Loading**: Images are automatically lazy-loaded
3. **Debounced Scrolling**: Scroll events are optimized for performance
4. **Memory Management**: Event listeners are properly cleaned up

## Troubleshooting

### Common Issues

1. **Images not loading**: Check image URLs and CORS settings
2. **Carousel not scrolling**: Ensure container has proper dimensions
3. **Navigation not working**: Verify categories array is not empty
4. **Responsive issues**: Check CSS media queries and viewport meta tag

### Debug Mode

Add `data-debug="true"` to see carousel state:

```tsx
<CategoryListSection 
  categories={categories}
  data-debug="true"
/>
```

## Contributing

When modifying this component:

1. Maintain accessibility standards
2. Test on multiple screen sizes
3. Ensure smooth performance
4. Update documentation
5. Add TypeScript types for new props

## License

This component is part of the KitPraid project and follows the project's licensing terms.
