export function riskTextClassColor(risk: number): string {
  if (risk < 30) return 'text-green-500';
  if (risk < 70) return 'text-yellow-500';
  return 'text-red-500';
}

export function riskBackgroundClassColor(risk: number): string {
  if (risk < 30) return 'bg-green-500';
  if (risk < 70) return 'bg-yellow-500';
  return 'bg-red-500';
}
